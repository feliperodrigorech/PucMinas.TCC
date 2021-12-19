using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Domain.Services;
using PucMinas.TCC.RegistrationInformation.Models;
using PucMinas.TCC.Utility.Constants;
using PucMinas.TCC.Utility.Extensions;
using PucMinas.TCC.Utility.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Services
{
    public class ChargeService
    {
        readonly IConfiguration Configuration;
        readonly IAddressRepository AddressRepository;
        readonly IChargeRepository ChargeRepository;
        readonly ICustomerRepository CustomerRepository;
        readonly IMerchandiseRepository MerchandiseRepository;
        readonly IPartyRepository PartyRepository;
        readonly IChargeHistoryRepository ChargeHistoryRepository;
        readonly IStepRepository StepRepository;

        readonly DistributedCacheService DistributedCacheService;
        readonly MessageQueueEmiteService MessageQueueEmiteService;

        public ChargeService(
            IConfiguration configuration,
            IAddressRepository addressRepository,
            IChargeRepository chargeRepository,
            ICustomerRepository customerRepository,
            IMerchandiseRepository merchandiseRepository,
            IPartyRepository partyRepository,
            IChargeHistoryRepository chargeHistoryRepository,
            IStepRepository stepRepository,
            IDistributedCache distributedCache
        )
        {
            Configuration = configuration;
            AddressRepository = addressRepository;
            ChargeRepository = chargeRepository;
            CustomerRepository = customerRepository;
            MerchandiseRepository = merchandiseRepository;
            PartyRepository = partyRepository;
            ChargeHistoryRepository = chargeHistoryRepository;
            StepRepository = stepRepository;

            DistributedCacheService = new(distributedCache);
            MessageQueueEmiteService = new(configuration);
        }

        public async Task<ChargeResultModel> CreateServiceCharge(Models.ChargeModel charge)
        {
            ValidadeServiceCharge(charge);

            PartyModel partyProvider = await PartyRepository.FindByCnpjCpf(charge.ProviderCnpjCpf);
            if (partyProvider == null)
                throw new Exception("O fornecedor não está apto para realizar a criação da carga. Entre em contato com a administração.");

            Domain.Models.CustomerModel customerModel = await CreateOrUpdateCustomer(charge.Customer);
            Domain.Models.AddressModel addressModel = await CreateAddress(charge.Customer.Address, customerModel.Party_Id);
            Domain.Models.ChargeModel chargeModel = await CreateCharge(
                charge,
                partyProvider.Id.Value,
                customerModel.Party_Id,
                addressModel.Id.Value
            );

            await CreateMerchandise(charge.LstMerchandise, chargeModel.Id.Value);

            MessageQueueEmiteService.Publish(QueueNameConstant.IntegrationChargeByLegacy, JsonConvert.SerializeObject(new
            {
                ChargeId = chargeModel.Id.Value
            }));

            return new ChargeResultModel
            {
                ChargeId = chargeModel.Id.Value
            };
        }

        public async Task<IEnumerable<Domain.Models.ChargeModel>> FindByCustomer(string cnpjCpf)
        {
            cnpjCpf = cnpjCpf.RemoveCharactersExceptNumbers();

            IEnumerable<Domain.Models.ChargeModel> lstCharge = await DistributedCacheService.LoadByCache<IEnumerable<Domain.Models.ChargeModel>>(cnpjCpf);
            if (lstCharge != null && lstCharge.Any())
                return lstCharge;

            lstCharge = await ChargeRepository.FindByCustomer(cnpjCpf);

            if (lstCharge == null || !lstCharge.Any())
                throw new Exception("Não foram localizados entregas para o CNPJ/CPF informado.");

            foreach (var charge in lstCharge)
            {
                charge.LstChargeHistory = (await ChargeHistoryRepository.FindByCharge(charge.Id.Value)).ToList();

                foreach (var chargeHistory in charge.LstChargeHistory)
                    chargeHistory.Step = await StepRepository.Find(chargeHistory.Step_Id);
            }

            await DistributedCacheService.SaveInCache(cnpjCpf, lstCharge, TimeSpan.FromMinutes(Convert.ToInt32(Configuration["DistributedCacheChargeHistoryCustomer"])));

            return lstCharge;
        }

        private void ValidadeServiceCharge(Models.ChargeModel charge)
        {
            string messageError = string.Empty;

            if (charge == null)
                messageError += "Deve ser informado os dados da carga para a solicitação.\n";

            if (!charge.TransDate.HasValue)
                messageError += "Deve ser informado os dados da carga para a solicitação.\n";

            if (charge.TransDate.HasValue && charge.TransDate.Value.Date > DateTime.Today.Date)
                messageError += "A data de venda não pode ser superior ao do dia atual.\n";

            if (string.IsNullOrEmpty(charge.ProviderCnpjCpf))
                messageError += "O CNPJ/CPF deve ser informado.\n";

            if (string.IsNullOrEmpty(charge.ProviderCnpjCpf) || !charge.ProviderCnpjCpf.IsValid())
                messageError += "O CNPJ/CPF do provedor deve ser válido.\n";

            if (string.IsNullOrEmpty(charge.SerialNumber))
                messageError += "O número da fiscal deve ser informado.\n";

            if (charge.Customer == null)
                messageError += "Deve ser informado o cliente para a entrega.\n";

            if (string.IsNullOrEmpty(charge.Customer?.Name))
                messageError += "O nome do destinatário deve ser informado.\n";

            if (string.IsNullOrEmpty(charge.Customer?.CnpjCpf) || !charge.Customer.CnpjCpf.IsValid())
                messageError += "O CNPJ/CPF do destinatário deve ser informado.\n";

            messageError += ValidateAddress(charge.Customer?.Address);

            messageError += ValidateMerchandise(charge.LstMerchandise);

            if (!string.IsNullOrEmpty(messageError))
                throw new Exception(messageError);
        }

        private string ValidateAddress(Models.AddressModel address)
        {
            string messageError = string.Empty;

            if (address == null)
                return "O endereço do destinatário deve ser informado.\n";

            if (string.IsNullOrEmpty(address?.ZipCode) && (address.Latitude.HasValue || !address.Longitude.HasValue))
                messageError += "O CEP ou a geolocalição do destinatário deve ser informado.\n";

            return messageError;
        }

        private string ValidateMerchandise(IList<Models.MerchandiseModel> lstMerchandise)
        {
            if (lstMerchandise == null || !lstMerchandise.Any())
                return "Deve ser informdo os produtos que serão enviados na carga.\n";

            string messageError = string.Empty;

            foreach (var merchandise in lstMerchandise)
            {
                if (string.IsNullOrEmpty(merchandise.Sku))
                    messageError += "O SKU deve ser informado.\n";

                if (!merchandise.Height.HasValue || !merchandise.Width.HasValue || !merchandise.Length.HasValue)
                    messageError += "As dimensões do produto devem ser informadas.\n";

                if (!merchandise.Weight.HasValue)
                    messageError += "O peso do produto deve ser informado.\n";

                if (!merchandise.PriceUnit.HasValue)
                    messageError += "O preço unitário do produto deve ser informado.\n";

                if (!merchandise.Quantity.HasValue)
                    messageError += "A quantidade de produtos deve ser informada.\n";
            }

            return messageError;
        }

        private async Task<Domain.Models.CustomerModel> CreateOrUpdateCustomer(Models.CustomerModel customer)
        {
            PartyModel party = await PartyRepository.FindByCnpjCpf(customer.CnpjCpf);
            Domain.Models.CustomerModel customerModel;
            if (party == null)
            {
                party = new()
                {
                    CnpjCpf = customer.CnpjCpf,
                    Description = customer.Description,
                    Name = customer.Name,
                    PartyType = customer.PartyType
                };

                await PartyRepository.Save(party);

                customerModel = new()
                {
                    Party_Id = party.Id.Value
                };

                await CustomerRepository.Save(customerModel);

                return customerModel;
            }

            customerModel = await CustomerRepository.FindByPartyId(party.Id);

            party.Description = customer.Description;
            party.Name = customer.Name;

            await PartyRepository.Update(party);

            return customerModel;
        }

        private async Task<Domain.Models.AddressModel> CreateAddress(Models.AddressModel address, long partyId)
        {
            Domain.Models.AddressModel addressModel = new()
            {
                Party_Id = partyId,
                ZipCode = address.ZipCode,
                Street = address.Street,
                StreetNumber = address.StreetNumber,
                BuildingCompliment = address.BuildingCompliment,
                District = address.District,
                City = address.City,
                State = address.State,
                Country = address.Country,
                Latitude = address.Latitude,
                Longitude = address.Longitude
            };

            await AddressRepository.Save(addressModel);

            return addressModel;
        }

        private async Task<Domain.Models.ChargeModel> CreateCharge(
            Models.ChargeModel charge,
            long originCustomerId,
            long destinationCustomerId,
            long destinationAddressId
        )
        {
            Domain.Models.ChargeModel chargeModel = new()
            {
                Notes = charge.Notes,
                SerialNumber = charge.SerialNumber,
                ServiceOrder = charge.ServiceOrder,
                TransDate = charge.TransDate.Value,
                OriginCustomer_Id = originCustomerId,
                DestinationCustomer_Id = destinationCustomerId,
                DestinationAddress_Id = destinationAddressId,
            };

            await ChargeRepository.Save(chargeModel);
            return chargeModel;
        }

        private async Task CreateMerchandise(IList<Models.MerchandiseModel> lstMerchandise, long chargeId)
        {
            foreach (var merchandise in lstMerchandise)
            {
                await MerchandiseRepository.Save(new()
                {
                    PriceUnit = merchandise.PriceUnit.Value,
                    Sku = merchandise.Sku,
                    UnitType = merchandise.UnitType.Value,
                    Quantity = merchandise.Quantity.Value,
                    Dimension = new()
                    {
                        Height = merchandise.Height,
                        Width = merchandise.Width,
                        Length = merchandise.Length,
                        Weight = merchandise.Weight
                    },
                    Charge_Id = chargeId
                });
            }
        }
    }
}
