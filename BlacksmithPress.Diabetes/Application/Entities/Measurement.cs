using BlacksmithPress.Diabetes.Types;
using NodaTime;

namespace BlacksmithPress.Diabetes.Entities
{
    /// <summary>
    /// A measurement
    /// </summary>
    /// <seealso cref="BlacksmithPress.Diabetes.Entities.Entity{long}" />
    /// <seealso cref="BlacksmithPress.Diabetes.Types.IMeasurement" />
    public class Measurement : Entity<long>, IMeasurement
    {
        /// <summary>
        /// Gets or sets the instant in time this measurement occurred.
        /// </summary>
        /// <value>The timestamp.</value>
        public Instant Timestamp { get; set; }
        /// <summary>
        /// Gets or sets the subject whose attribute was being measured.
        /// </summary>
        /// <value>The subject.</value>
        public IPerson Subject { get; set; }
        /// <summary>
        /// Gets or sets the attribute being measured.
        /// </summary>
        /// <value>The attribute.</value>
        public MeasuredAttribute Attribute { get; set; } = MeasuredAttribute.Glucose;
        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure.</value>
        public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.mgdL;
        /// <summary>
        /// Gets or sets the amount measured.
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }
    }
}