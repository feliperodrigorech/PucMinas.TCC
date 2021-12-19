namespace PucMinas.TCC.Domain.Models
{
    public class ProviderModel
    {
        public long Party_Id { get; set; }
        
        public PartyModel Party { get; set; }
    }
}
