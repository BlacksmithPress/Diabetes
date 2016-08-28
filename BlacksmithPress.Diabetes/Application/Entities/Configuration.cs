using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Entities
{
    public class Configuration : Singleton<Configuration>, IConfiguration
    {
        public Uri PersistenceUri => new Uri("http://diabetescloud.azurewebsites.net/Api/");

        private IContainer container;

        public IContainer Container
        {
            get
            {
                if (container != null) return container;

                var builder = new ContainerBuilder();
                builder.RegisterType<Person>().As<IPerson>();
                builder.RegisterInstance(this).As<IConfiguration>();
                container = builder.Build();
                return container;
            }
        }
    }
}
