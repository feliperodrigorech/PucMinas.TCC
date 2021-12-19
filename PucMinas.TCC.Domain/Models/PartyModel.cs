using System.Collections.Generic;

namespace PucMinas.TCC.Domain.Models
{
    public class PartyModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CnpjCpf { get; set; }
        public PartyType PartyType { get; set; }
        public IList<AddressModel> LstAddress { get; set; }
        public IList<ContactModel> LstContact { get; set; }
    }
}
