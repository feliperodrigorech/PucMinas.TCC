namespace PucMinas.TCC.Domain.Models
{
    public class MerchandiseModel : BaseModel
    {
        public decimal PriceUnit { get; set; }
        public decimal Quantity { get; set; }
        public string Sku { get; set; }
        public UnitType UnitType { get; set; }
        public long Charge_Id { get; set; }
        public long? Wharehouse_Id { get; set; }

        public ChargeModel Charge { get; set; }
        public WarehouseModel Warehouse { get; set; }
        public DimensionModel Dimension { get; set; }
    }
}
