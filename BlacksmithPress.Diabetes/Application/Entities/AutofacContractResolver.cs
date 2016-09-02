using System;
using Autofac;
using Newtonsoft.Json.Serialization;

namespace BlacksmithPress.Diabetes.Entities
{
    /// <summary>
    /// A contract resolver, used by JSON.NET, to construct objects using the dependency injection container.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Serialization.DefaultContractResolver" />
    public class AutofacContractResolver : DefaultContractResolver
    {
        private IContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacContractResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AutofacContractResolver(IContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract" /> for the given type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract" /> for the given type.</returns>
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            if (container.IsRegistered(objectType))
                contract.DefaultCreator = () => container.Resolve(objectType);

            return contract;
        }
    }
}
