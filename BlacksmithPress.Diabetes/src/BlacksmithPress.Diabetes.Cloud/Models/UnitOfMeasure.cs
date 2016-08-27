namespace BlacksmithPress.Diabetes.Cloud.Models
{
    public class UnitOfMeasure
    {
        private UnitOfMeasure() {}

        public static UnitOfMeasure mgdl => new UnitOfMeasure() {Id = 1, Name = "mg/dl"};
        public static UnitOfMeasure mmoll => new UnitOfMeasure() {Id = 2, Name = "mmol/l"};
        public int Id { get; set; }
        public string Name { get; set; }
    }
}