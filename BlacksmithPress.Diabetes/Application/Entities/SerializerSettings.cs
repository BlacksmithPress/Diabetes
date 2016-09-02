using BlacksmithPress.Diabetes.Types;
using Newtonsoft.Json;

namespace BlacksmithPress.Diabetes.Entities
{
    /// <summary>
    /// Settings used for serialization and deserializing objects.
    /// </summary>
    /// <seealso cref="BlacksmithPress.Diabetes.Types.Singleton{BlacksmithPress.Diabetes.Entities.SerializerSettings}" />
    public class SerializerSettings : Singleton<SerializerSettings>
    {
        private IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializerSettings"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public SerializerSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializerSettings"/> class.
        /// </summary>
        public SerializerSettings() : this(Configuration.Instance) {}

        /// <summary>
        /// Gets the JSON.NET serializer settings object that represents the Blacksmith Press serialization settings.
        /// </summary>
        /// <value>The json.</value>
        public JsonSerializerSettings Json => new JsonSerializerSettings
        {
            ContractResolver = new AutofacContractResolver(configuration.Container),
        };
    }
}
