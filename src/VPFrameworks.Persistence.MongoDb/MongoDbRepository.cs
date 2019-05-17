using Frameworks.DDD;
using Frameworks.Persistence.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Persistence.MongoDb
{
    public abstract class MongoDbRepository<TId, TEntity, TPartialUpdate> : IReadRepository<TId, TEntity>, IWriteRepository<TId, TEntity, TPartialUpdate> where TEntity : Entity<TId>
    {
        IMongoCollection<TEntity> collection;
        FilterDefinitionBuilder<TEntity> filterBuilder = new FilterDefinitionBuilder<TEntity>();

        public MongoDbRepository(IOptions<DatabaseSettings> options)
        {

            this.collection = new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.DataBaseName).GetCollection<TEntity>(options.Value.SetName);
        }
        
        public async Task Create(TEntity entity, CancellationToken token)
        {
            await this.collection.InsertOneAsync(entity,new InsertOneOptions() {  BypassDocumentValidation = true }, token);
        }

        public async Task Delete(TId id, CancellationToken token)
        {
           
            FilterDefinition<TEntity> definition = filterBuilder.Eq<TId>((entity) => entity.Id, id);

            await this.collection.DeleteOneAsync(definition, token);
        }

        public async Task<TEntity> Get(TId id, CancellationToken token)
        {
            FilterDefinition<TEntity> definition = filterBuilder.Eq<TId>((entity) => entity.Id, id);
            var cursor = await this.collection.FindAsync<TEntity>(definition);

            return await cursor.FirstOrDefaultAsync(token);
        }

         

        public async Task<IEnumerable<TEntity>> GetByTags(IEnumerable<string> containingTags, int take, int skip, CancellationToken token)
        {
            var filterDefinition = filterBuilder.ElemMatch((entity) => entity.Tags, (tag) => containingTags.Contains(tag));

            var cursor = await this.collection.FindAsync<TEntity>(filterDefinition, new FindOptions<TEntity, TEntity>() { Skip = skip, Limit = take });

            return await cursor.ToListAsync(token);

        }

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

        public async Task InsertOrUpdate(TId id, TPartialUpdate entity, CancellationToken token)
        {
            UpdateDefinitionBuilder<TEntity> updateDefinitionBuilder = new UpdateDefinitionBuilder<TEntity>();

            UpdateDefinition<TEntity> updateDefinition = this.GetUpdateDefinition(entity);

            FilterDefinition<TEntity> filterById = filterBuilder.Eq<TId>((_entity) => _entity.Id, id);

            await this.collection.UpdateOneAsync(filterById, updateDefinition, new UpdateOptions() { IsUpsert = true });
        }

        protected abstract UpdateDefinition<TEntity> GetUpdateDefinition(TPartialUpdate entity);
        
    }
}
