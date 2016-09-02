using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Entities
{
    /// <summary>
    /// The configuration required by any Blacksmith Press Diabetes component.
    /// </summary>
    /// <seealso cref="BlacksmithPress.Diabetes.Types.Singleton{BlacksmithPress.Diabetes.Entities.Configuration}" />
    /// <seealso cref="BlacksmithPress.Diabetes.Types.IConfiguration" />
    public class Configuration : Singleton<Configuration>, IConfiguration
    {
        public Uri PersistenceUri => new Uri("http://diabetescloud.azurewebsites.net/Api/");

        private IContainer container;

        /// <summary>
        /// Gets the dependency injection container used for constructing entities.
        /// </summary>
        /// <value>The container.</value>
        public IContainer Container
        {
            get
            {
                if (container != null) return container;

                var builder = new ContainerBuilder();
                builder.RegisterType<Person>().As<IPerson>();
                builder.RegisterInstance(this).As<IConfiguration>();
                builder.RegisterType<Measurement>().As<IMeasurement>();
                container = builder.Build();
                return container;
            }
        }
    }
}
