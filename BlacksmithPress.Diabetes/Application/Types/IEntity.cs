using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Types
{
    /// <summary>
    /// This interface defines the basis for all entities in the Blacksmith Press Diabetes application.
    /// </summary>
    /// <typeparam name="KeyType">The type of the entity's primary key.</typeparam>
    public interface IEntity<KeyType> where KeyType : struct 
    {
        /// <summary>
        /// Gets or sets the entity's identifier (primary key).
        /// </summary>
        /// <value>The identifier.</value>
        KeyType Id { get; set; }
    }
}
