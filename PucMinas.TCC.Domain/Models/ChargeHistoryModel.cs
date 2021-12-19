namespace PucMinas.TCC.Domain.Models
{
    public class ChargeHistoryModel : BaseModel
    {
        public string Description { get; set; }
        public long Charge_Id { get; set; }
        public long Step_Id { get; set; }

        public ChargeModel Charge { get; set; }
        public StepModel Step { get; set; }
    }
}
