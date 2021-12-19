using System.Collections.Generic;

namespace PucMinas.TCC.Domain.Models
{
    public class CustomerModel : BaseModel
    {
        public long Party_Id { get; set; }

        public PartyModel Party { get; set; }
        public IList<MerchandiseModel> LstMerchandise { get; set; }
    }
}
