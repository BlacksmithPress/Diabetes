using System;
using NodaTime;

namespace BlacksmithPress.Diabetes.Types
{
    public interface IMeasurement {
        Guid Id { get; set; }
        Instant Timestamp { get; set; }
        IPerson Subject { get; set; }
        MeasuredAttribute Attribute { get; set; }
        UnitOfMeasure UnitOfMeasure { get; set; }
        decimal Amount { get; set; }
    }
}