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


        protected static IContainer BuildDefaultContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Configuration>().As<IConfiguration>();
            builder.RegisterType<Person>().As<IPerson>();
            builder.RegisterType<Measurement>().As<IMeasurement>();
            return builder.Build();
        }


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

        public virtual async Task<EntityType> CreateAsync(EntityType entity)
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

        public virtual EntityType Create(EntityType entity)
        {
            return CreateAsync(entity).Result;
        }

        public virtual async Task<EntityType> GetAsync(KeyType key)
        {
            var response = await Client.GetAsync($"{relativeUri}{key}");
            if (!response.IsSuccessStatusCode)
                return default(EntityType);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EntityType>(json, SerializerSettings.Instance.Json);
        }

        public virtual EntityType Get(KeyType key)
        {
            return GetAsync(key).Result;
        }

        public virtual async Task<IEnumerable<EntityType>> GetAllAsync()
        {
            var response = await Client.GetAsync(relativeUri);
            if (!response.IsSuccessStatusCode)
                return default(IQueryable<EntityType>);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<EntityType>>(json, SerializerSettings.Instance.Json);
        }

        public virtual IEnumerable<EntityType> GetAll()
        {
            return GetAllAsync().Result;
        }

        public virtual async Task<EntityType> UpdateAsync(EntityType entity)
        {
            var json = JsonConvert.SerializeObject(entity, SerializerSettings.Instance.Json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{relativeUri}{entity.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EntityType>(json, SerializerSettings.Instance.Json);
            }

            throw new InvalidDataException($"Exception while updating {typeof(EntityType).Name}: \"{response.ReasonPhrase}\".");
        }

        public virtual EntityType Update(EntityType entity)
        {
            return UpdateAsync(entity).Result;
        }

        public virtual async Task DeleteAsync(KeyType key)
        {
            var response = await Client.DeleteAsync($"{relativeUri}{key}");

            if (!response.IsSuccessStatusCode)
                throw new InvalidDataException($"Exception while deleting {typeof(EntityType).Name}: \"{response.ReasonPhrase}\".");
        }

        public virtual void Delete(KeyType key)
        {
            DeleteAsync(key).Wait();
        }
    }
}
