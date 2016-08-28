using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Entities;
using BlacksmithPress.Diabetes.Types;
using Newtonsoft.Json;

namespace BlacksmithPress.Diabetes.Persistence.Repositories
{
    public abstract class Repository<EntityType, KeyType> where EntityType : IEntity<KeyType> where KeyType : struct 
    {
        private IContainer container;
        private IConfiguration configuration;
        private string relativeUri;

        public Repository(IContainer container, string uri)
        {
            this.container = container;
            this.configuration = this.container.Resolve<IConfiguration>();
            this.relativeUri = uri;
        }

        protected HttpClient Client
        {
            get
            {
                var client = new HttpClient
                {
                    BaseAddress = configuration.PersistenceUri,
                };
                return client;
            }
        }

        public virtual async Task<EntityType> Create(EntityType entity)
        {
            var json = JsonConvert.SerializeObject(entity, SerializerSettings.Instance.Json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(relativeUri, content);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EntityType>(json, SerializerSettings.Instance.Json);
            }

            throw new InvalidDataException($"Exception while creating {typeof(EntityType).Name}: \"{response.ReasonPhrase}\".");
        }

        public virtual EntityType Get(KeyType key)
        {
            return default(EntityType);
        }

        public virtual EntityType Update(EntityType entity)
        {
            return entity;
        }

        public virtual void Delete(KeyType key)
        {
            ;
        }
    }
}
