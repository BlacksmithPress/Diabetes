using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Entities
{
    /// <summary>
    /// A person
    /// </summary>
    /// <seealso cref="BlacksmithPress.Diabetes.Entities.Entity{long}" />
    /// <seealso cref="BlacksmithPress.Diabetes.Types.IPerson" />
    public class Person : Entity<long>, IPerson
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }
}
