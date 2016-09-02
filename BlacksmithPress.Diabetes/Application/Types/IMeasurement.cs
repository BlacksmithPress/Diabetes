using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace BlacksmithPress.Diabetes.Types
{
    /// <summary>
    /// The interface for all measurements
    /// </summary>
    /// <seealso cref="BlacksmithPress.Diabetes.Types.IEntity{long}" />
    public interface IMeasurement : IEntity<long>
    {
        /// <summary>
        /// Gets or sets the instant in time this measurement occurred.
        /// </summary>
        /// <value>The timestamp.</value>
        Instant Timestamp { get; set; }
        /// <summary>
        /// Gets or sets the subject whose attribute was being measured.
        /// </summary>
        /// <value>The subject.</value>
        IPerson Subject { get; set; }
        /// <summary>
        /// Gets or sets the attribute being measured.
        /// </summary>
        /// <value>The attribute.</value>
        MeasuredAttribute Attribute { get; set; }
        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure.</value>
        UnitOfMeasure UnitOfMeasure { get; set; }
        /// <summary>
        /// Gets or sets the amount measured.
        /// </summary>
        /// <value>The amount.</value>
        decimal Amount { get; set; }
    }
}
