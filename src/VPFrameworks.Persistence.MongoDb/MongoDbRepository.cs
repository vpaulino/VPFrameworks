

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VPFrameworks.Persistence.Abstractions;

namespace Azure.Persistence.MongoDb
{
    /// <summary>
    /// Data access to mongodb repository
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPartialUpdate"></typeparam>
    public abstract class MongoDbRepository<TId, TEntity, TPartialUpdate> : IReadRepository<TId, TEntity>, IWriteRepository<TId, TEntity, TPartialUpdate> where TEntity : Entity<TId>
    {
        IMongoCollection<TEntity> collection;
        FilterDefinitionBuilder<TEntity> filterBuilder = new FilterDefinitionBuilder<TEntity>();

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="options"></param>
        public MongoDbRepository(IOptions<DatabaseSettings> options)
        {

            this.collection = new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.DataBaseName).GetCollection<TEntity>(options.Value.SetName);
        }
        
        /// <summary>
        /// Adds a new object to the collection
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Create(TEntity entity, CancellationToken token)
        {
            await this.collection.InsertOneAsync(entity,new InsertOneOptions() {  BypassDocumentValidation = true }, token);
        }

        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Delete(TId id, CancellationToken token)
        {
           
            FilterDefinition<TEntity> definition = filterBuilder.Eq<TId>((entity) => entity.Id, id);

            await this.collection.DeleteOneAsync(definition, token);
        }

        /// <summary>
        /// Gets the entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TEntity> Get(TId id, CancellationToken token)
        {
            FilterDefinition<TEntity> definition = filterBuilder.Eq<TId>((entity) => entity.Id, id);
            var cursor = await this.collection.FindAsync<TEntity>(definition);

            return await cursor.FirstOrDefaultAsync(token);
        }

         
        /// <summary>
        /// Gets all the entities that have those tags
        /// </summary>
        /// <param name="containingTags"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetByTags(IEnumerable<string> containingTags, int take, int skip, CancellationToken token)
        {
            var filterDefinition = filterBuilder.ElemMatch((entity) => entity.Tags, (tag) => containingTags.Contains(tag));

            var cursor = await this.collection.FindAsync<TEntity>(filterDefinition, new FindOptions<TEntity, TEntity>() { Skip = skip, Limit = take });

            return await cursor.ToListAsync(token);

        }

        /// <summary>
        /// get entityes that have those tags and were created in that time interval
        /// </summary>
        /// <param name="containingTags"></param>
        /// <param name="CreatedBiggerThen"></param>
        /// <param name="CreatedlessThen"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetByTagsTimeInterval(IEnumerable<string> containingTags, DateTime CreatedBiggerThen, DateTime CreatedlessThen , int take, int skip, CancellationToken token = default)
        {
            var tagsFilterDefinition = filterBuilder.ElemMatch((entity) => entity.Tags, (tag) => containingTags.Contains(tag));
            var datesInterval = filterBuilder.And(filterBuilder.Gte<DateTime>((entity) => entity.Created, CreatedBiggerThen), filterBuilder.Lte<DateTime>((entity) => entity.Created, CreatedlessThen));
            var searchOptions = new FindOptions<TEntity, TEntity>()
            {
                Skip = skip,
                Limit = take,
                Sort = Builders<TEntity>.Sort.Descending((entity)=> entity.Created),
            };
            
            var cursor = await this.collection.FindAsync<TEntity>(filterBuilder.And(tagsFilterDefinition, datesInterval), searchOptions);
            
            return await cursor.ToListAsync(token);

        }
        /// <summary>
        /// Get entities that were creates in this interval of time
        /// </summary>
        /// <param name="CreatedBiggerThen"></param>
        /// <param name="CreatedlessThen"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetByTimeInterval(DateTime CreatedBiggerThen, DateTime CreatedlessThen, int take, int skip, CancellationToken token = default)
        {
            var datesInterval = filterBuilder.And(filterBuilder.Gte<DateTime>((entity) => entity.Created, CreatedBiggerThen), filterBuilder.Lte<DateTime>((entity) => entity.Created, CreatedlessThen));
            var searchOptions = new FindOptions<TEntity, TEntity>()
            {
                Skip = skip,
                Limit = take,
                Sort = Builders<TEntity>.Sort.Descending((entity) => entity.Created),
            };

            var cursor = await this.collection.FindAsync<TEntity>(datesInterval, searchOptions);

            return await cursor.ToListAsync(token);
        }

        /// <summary>
        /// List entities by page
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetList<TOrderKey>(int take, int skip, Func<TEntity, TOrderKey> orderBy, int orderDirection, CancellationToken token)
        {
            var cursor = await this.collection.FindAsync<TEntity>(filterBuilder.Empty, new FindOptions<TEntity, TEntity>() { Skip = skip, Limit = take });

            var results = await cursor.ToListAsync(token);

            return results.OrderBy(orderBy);
        }

        /// <summary>
        /// Insert if does not exists or updates if exists
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task InsertOrUpdate(TId id, TPartialUpdate entity, CancellationToken token)
        {
            UpdateDefinitionBuilder<TEntity> updateDefinitionBuilder = new UpdateDefinitionBuilder<TEntity>();

            UpdateDefinition<TEntity> updateDefinition = this.GetUpdateDefinition(entity);

            FilterDefinition<TEntity> filterById = filterBuilder.Eq<TId>((_entity) => _entity.Id, id);

            await this.collection.UpdateOneAsync(filterById, updateDefinition, new UpdateOptions() { IsUpsert = true });
        }

        /// <summary>
        /// Child classes must implement how some specific entity can be updated
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract UpdateDefinition<TEntity> GetUpdateDefinition(TPartialUpdate entity);
        
    }
}
