namespace PucMinas.TCC.Domain.Models
{
    public class ContactModel : BaseModel
    {
        public string Description { get; set; }
        public string Locator { get; set; }
        public ContactType ContactType { get; set; }
        public bool IsPrimary { get; set; }
    }
}
