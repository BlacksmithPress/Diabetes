using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Types
{
    /// <summary>
    /// The interface defining a person in the Blacksmith Diabetes application.
    /// </summary>
    /// <seealso cref="BlacksmithPress.Diabetes.Types.IEntity{long}" />
    public interface IPerson : IEntity<long>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
    }
}
