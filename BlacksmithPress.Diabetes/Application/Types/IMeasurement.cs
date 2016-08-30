using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace BlacksmithPress.Diabetes.Types
{
    public interface IMeasurement : IEntity<long>
    {
        Instant Timestamp { get; set; }
        IPerson Subject { get; set; }
        MeasuredAttribute Attribute { get; set; }
        UnitOfMeasure UnitOfMeasure { get; set; }
        decimal Amount { get; set; }
    }
}
