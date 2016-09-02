using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace BlacksmithPress.Diabetes.Types
{
    /// <summary>
    /// The interface defining the configuration values required for any Blacksmith Press Diabetes component.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the base URI for the persistence API.
        /// </summary>
        /// <value>The persistence URI.</value>
        Uri PersistenceUri { get; }
        /// <summary>
        /// Gets the dependency injection container used for constructing entities.
        /// </summary>
        /// <value>The container.</value>
        IContainer Container { get; }
    }
}
