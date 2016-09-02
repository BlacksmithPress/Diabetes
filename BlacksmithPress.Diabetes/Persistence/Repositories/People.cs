using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Entities;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Persistence.Repositories
{
    /// <summary>
    /// Repository for managing people. Provides basic CRUD operations for <see cref="IPerson"/> entities.
    /// </summary>
    /// <seealso cref="BlacksmithPress.Diabetes.Persistence.Repositories.Repository{BlacksmithPress.Diabetes.Types.IPerson, System.Int64}" />
    public class People : Repository<IPerson, long>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="People"/> class using a default container.
        /// </summary>
        public People() : this(BuildDefaultContainer()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="People"/> class.
        /// </summary>
        /// <param name="container">The container to use for constructing entities.</param>
        public People(IContainer container) : base(container, "people/") {}
    }
}
