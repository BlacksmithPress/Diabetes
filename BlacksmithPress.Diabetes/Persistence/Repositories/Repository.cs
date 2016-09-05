using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Entities;
using BlacksmithPress.Diabetes.Types;
using Newtonsoft.Json;

namespace BlacksmithPress.Diabetes.Persistence.Repositories
{
    /// <summary>
    /// Base class for generic entity repository implementations. Provides basic CRUD operations for Blacksmith Press Diabetes entities.
    /// </summary>
    /// <typeparam name="EntityType">The concrete type of the entity managed by this repository.</typeparam>
    /// <typeparam name="KeyType">The type of this entity's primary key. This type must be a value-type.</typeparam>
    public abstract class Repository<EntityType, KeyType> where EntityType : IEntity<KeyType> where KeyType : struct 
    {
        private IContainer container;
        private IConfiguration configuration;
        private string relativeUri;
        private NetworkCredential credentials;

        /// <summary>
        /// Builds a default container that defines the entity implementations used by this repositories library.
        /// </summary>
        /// <returns>IContainer.</returns>
        protected static IContainer BuildDefaultContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Configuration>().As<IConfiguration>();
            builder.RegisterType<Person>().As<IPerson>();
            builder.RegisterType<Measurement>().As<IMeasurement>();
            return builder.Build();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{EntityType, KeyType}"/> class.
        /// </summary>
        /// <param name="container">The container to use for constructing entities.</param>
        /// <param name="uri">The URI of the persistence API.</param>
        public Repository(IContainer container, string uri, NetworkCredential credentials)
        {
            this.container = container;
            this.configuration = this.container.Resolve<IConfiguration>();
            this.relativeUri = uri;
            this.credentials = credentials;
        }

        /// <summary>
        /// Gets the HTTP client used to communicate with the API.
        /// </summary>
        /// <value>The client.</value>
        protected HttpClient Client
        {
            get
            {
                var client = new HttpClient
                {
                    BaseAddress = configuration.PersistenceUri,
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    this.credentials.ToBasicAuthentication());
                return client;
            }
        }

        /// <summary>
        /// Create as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity to create in the repository.</param>
        /// <returns>Task&lt;EntityType&gt;.</returns>
        /// <exception cref="InvalidDataException">The API returned a non-success result, while creating the entity.</exception>
        public virtual async Task<EntityType> CreateAsync(EntityType entity)
        {
            var json = JsonConvert.SerializeObject(entity, SerializerSettings.Instance.Json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(relativeUri, content);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<EntityType>(json, SerializerSettings.Instance.Json);
                return entity;
            }

            throw new InvalidDataException($"The API returned a non-success result, while creating the {typeof(EntityType).Name}: [{response.StatusCode}] \"{response.ReasonPhrase}\".");
        }

        /// <summary>
        /// Creates the specified entity in the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>EntityType.</returns>
        /// <exception cref="InvalidDataException">The API returned a non-success result, while creating the entity.</exception>
        public virtual EntityType Create(EntityType entity)
        {
            return CreateAsync(entity).Result;
        }

        /// <summary>
        /// Gets one entity as an asynchronous operation.
        /// </summary>
        /// <param name="key">The primary key of the entity to retrieve.</param>
        /// <returns>Task&lt;EntityType&gt;.</returns>
        public virtual async Task<EntityType> GetAsync(KeyType key)
        {
            var response = await Client.GetAsync($"{relativeUri}{key}");
            if (!response.IsSuccessStatusCode)
                return default(EntityType);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EntityType>(json, SerializerSettings.Instance.Json);
        }

        /// <summary>
        /// Gets one entity with the specified key.
        /// </summary>
        /// <param name="key">The primary key of the entity to retrieve.</param>
        /// <returns>EntityType.</returns>
        public virtual EntityType Get(KeyType key)
        {
            return GetAsync(key).Result;
        }

        /// <summary>
        /// Gets all stored instances of the entity as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;EntityType&gt;&gt;.</returns>
        public virtual async Task<IEnumerable<EntityType>> GetAllAsync()
        {
            var response = await Client.GetAsync(relativeUri);
            if (!response.IsSuccessStatusCode)
                return default(IQueryable<EntityType>);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<EntityType>>(json, SerializerSettings.Instance.Json);
        }

        /// <summary>
        /// Gets all stored instances of the entity.
        /// </summary>
        /// <returns>IEnumerable&lt;EntityType&gt;.</returns>
        public virtual IEnumerable<EntityType> GetAll()
        {
            return GetAllAsync().Result;
        }

        /// <summary>
        /// Updates the specified existing entity as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>Task&lt;EntityType&gt;.</returns>
        /// <exception cref="InvalidDataException">The API returned a non-success result, while updating the entity.</exception>
        public virtual async Task<EntityType> UpdateAsync(EntityType entity)
        {
            var json = JsonConvert.SerializeObject(entity, SerializerSettings.Instance.Json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{relativeUri}{entity.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<EntityType>(json, SerializerSettings.Instance.Json);
                return entity;
            }

            throw new InvalidDataException($"Exception while updating {typeof(EntityType).Name}: \"{response.ReasonPhrase}\".");
        }

        /// <summary>
        /// Updates the specified existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>EntityType.</returns>
        /// <exception cref="InvalidDataException">The API returned a non-success result, while updating the entity.</exception>
        public virtual EntityType Update(EntityType entity)
        {
            return UpdateAsync(entity).Result;
        }

        /// <summary>
        /// Deletes the entity with the specified key as an asynchronous operation.
        /// </summary>
        /// <param name="key">The primary key of the entity to delete.</param>
        /// <returns>Task.</returns>
        /// <exception cref="InvalidDataException">The API returned a non-success result, while deleting the entity.</exception>
        public virtual async Task DeleteAsync(KeyType key)
        {
            var response = await Client.DeleteAsync($"{relativeUri}{key}");

            if (!response.IsSuccessStatusCode)
                throw new InvalidDataException($"Exception while deleting {typeof(EntityType).Name}: \"{response.ReasonPhrase}\".");
        }

        /// <summary>
        /// Deletes the entity with the specified key.
        /// </summary>
        /// <param name="key">The primary key of the entity to delete.</param>
        /// <exception cref="InvalidDataException">The API returned a non-success result, while deleting the entity.</exception>
        public virtual void Delete(KeyType key)
        {
            DeleteAsync(key).Wait();
        }
    }
}
