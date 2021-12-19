using PucMinas.TCC.Domain.Models;

namespace PucMinas.TCC.RegistrationInformation.Models
{
    public class MerchandiseModel
    {
        public decimal? PriceUnit { get; set; }
        public decimal? Quantity { get; set; }
        public string Sku { get; set; }
        public UnitType? UnitType { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
    }
}
