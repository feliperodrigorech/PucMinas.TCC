using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.RegistrationInformation.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PucMinas.TCC.RegistrationInformation.Controllers
{
    [Authorize( Roles = "Admin")]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class ChargeByCustomerController : ControllerBase
    {
        readonly ChargeService ChargeService;

        public ChargeByCustomerController(
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
            ChargeService = new ChargeService(
                configuration,
                addressRepository,
                chargeRepository,
                customerRepository,
                merchandiseRepository,
                partyRepository,
                chargeHistoryRepository,
                stepRepository,
                distributedCache
            );
        }

        [HttpGet("{cnpjCpf}")]
        public async Task<IActionResult> Get(string cnpjCpf)
        {
            ReturnModel<IEnumerable<ChargeModel>> returnModel = new();

            try
            {
                returnModel.Object = await ChargeService.FindByCustomer(cnpjCpf);
                return StatusCode((int)HttpStatusCode.OK, returnModel);
            }
            catch (Exception ex)
            {
                returnModel.SetError(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnModel);
            }
        }
    }
}
