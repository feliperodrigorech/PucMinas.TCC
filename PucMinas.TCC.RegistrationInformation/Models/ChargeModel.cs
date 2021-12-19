using System;
using System.Collections.Generic;

namespace PucMinas.TCC.RegistrationInformation.Models
{
    public class ChargeModel
    {
        public string Notes { get; set; }
        public string ServiceOrder { get; set; }
        public DateTime? TransDate { get; set; }
        public string SerialNumber { get; set; }

        public IList<MerchandiseModel> LstMerchandise { get; set; }
        public CustomerModel Customer { get; set; }
        public string ProviderCnpjCpf { get; set; }
    }

    public class ChargeResultModel
    {
        public long ChargeId { get; set; }
    }
}
