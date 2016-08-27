using System;

namespace BlacksmithPress.Diabetes.Cloud.Models
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Person Subject { get; set; }
        public MeasuredAttribute Attribute { get; set; } = MeasuredAttribute.Glucose;
        public UnitOfMeasure UnitOfMeasure { get; set; } = MeasuredAttribute.Glucose.DefaultUnitOfMeasure;
        public decimal Amount { get; set; }
    }
}