namespace PucMinas.TCC.RegistrationInformation.Models
{
    public class AddressModel
    {
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string BuildingCompliment { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
