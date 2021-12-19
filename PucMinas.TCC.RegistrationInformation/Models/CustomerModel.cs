using PucMinas.TCC.Domain.Models;

namespace PucMinas.TCC.RegistrationInformation.Models
{
    public class CustomerModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CnpjCpf { get; set; }
        public PartyType PartyType { get; set; }

        public AddressModel Address { get; set; }
    }
}
