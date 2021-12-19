using System;
using System.Collections.Generic;

namespace PucMinas.TCC.Domain.Models
{
    public class ChargeModel : BaseModel
    {
        public string Notes { get; set; }
        public string ServiceOrder { get; set; }
        public DateTime TransDate { get; set; }
        public string SerialNumber { get; set; }
        public long OriginCustomer_Id { get; set; }
        public long DestinationCustomer_Id { get; set; }
        public long DestinationAddress_Id { get; set; }

        public CustomerModel OriginCustomer { get; set; }
        public CustomerModel DestinationCustomer { get; set; }
        public AddressModel DestinationAddress { get; set; }
        public IList<MerchandiseModel> LstMerchandise { get; set; }
        public IList<ChargeHistoryModel> LstChargeHistory { get; set; }
    }
}
