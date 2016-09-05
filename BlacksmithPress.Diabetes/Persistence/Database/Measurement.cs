using System.ComponentModel.DataAnnotations.Schema;
using BlacksmithPress.Diabetes.Types;
using NodaTime;

namespace BlacksmithPress.Diabetes.Persistence.Database
{
    public class Measurement 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public Instant Timestamp { get; set; }
        public Person Subject { get; set; }
        public MeasuredAttribute Attribute { get; set; } = MeasuredAttribute.Glucose;
        public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.mgdL;
        public decimal Amount { get; set; }
    }
}