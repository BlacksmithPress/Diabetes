using System;
using Autofac;
using Newtonsoft.Json.Serialization;

namespace BlacksmithPress.Diabetes.Entities
{
    public class AutofacContractResolver : DefaultContractResolver
    {
        private IContainer container;

        public AutofacContractResolver(IContainer container)
        {
            this.container = container;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            if (container.IsRegistered(objectType))
                contract.DefaultCreator = () => container.Resolve(objectType);

            return contract;
        }
    }
}
