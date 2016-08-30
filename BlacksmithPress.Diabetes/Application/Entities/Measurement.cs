using BlacksmithPress.Diabetes.Types;
using NodaTime;

namespace BlacksmithPress.Diabetes.Entities
{
    public class Measurement : Entity<long>, IMeasurement
    {
        public Instant Timestamp { get; set; }
        public IPerson Subject { get; set; }
        public MeasuredAttribute Attribute { get; set; } = MeasuredAttribute.Glucose;
        public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.mgdL;
        public decimal Amount { get; set; }
    }
}