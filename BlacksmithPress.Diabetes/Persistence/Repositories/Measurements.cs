using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Net;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Persistence.Repositories
{
    /// <summary>
    /// Repository for managing measurements. Provides basic CRUD operations for <see cref="IMeasurement"/> entities.
    /// </summary>
    /// <seealso cref="BlacksmithPress.Diabetes.Persistence.Repositories.Repository{BlacksmithPress.Diabetes.Types.IMeasurement, System.Int64}" />
    public class Measurements : Repository<IMeasurement, long>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Measurements"/> class using a default container.
        /// </summary>
        public Measurements(NetworkCredential credentials) : this(BuildDefaultContainer(), credentials) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="Measurements"/> class.
        /// </summary>
        /// <param name="container">The container to use for constructing entities.</param>
        public Measurements(IContainer container, NetworkCredential credentials) : base(container, "measurements/", credentials) {}
    }
}
