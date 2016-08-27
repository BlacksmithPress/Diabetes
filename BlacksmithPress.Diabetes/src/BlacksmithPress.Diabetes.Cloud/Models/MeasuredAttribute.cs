namespace BlacksmithPress.Diabetes.Cloud.Models
{
    public class MeasuredAttribute
    {
        private MeasuredAttribute() {}
        public static MeasuredAttribute Glucose => new MeasuredAttribute() {Id = 1, Name = "glucose"};
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitOfMeasure DefaultUnitOfMeasure { get; set; } = UnitOfMeasure.mgdl;
    }
}