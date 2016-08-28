using BlacksmithPress.Diabetes.Types;
using Newtonsoft.Json;

namespace BlacksmithPress.Diabetes.Entities
{
    public class SerializerSettings : Singleton<SerializerSettings>
    {
        private IConfiguration configuration;

        public SerializerSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public SerializerSettings() : this(Configuration.Instance) {}

        public JsonSerializerSettings Json => new JsonSerializerSettings
        {
            ContractResolver = new AutofacContractResolver(configuration.Container),
        };
    }
}
