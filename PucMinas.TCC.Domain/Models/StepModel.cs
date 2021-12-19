namespace PucMinas.TCC.Domain.Models
{
    public class StepModel : BaseModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDelivered { get; set; }
    }
}
