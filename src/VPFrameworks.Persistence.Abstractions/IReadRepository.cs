using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VPFrameworks.Persistence.Abstractions
{
    /// <summary>
    /// This interfaces represents the common contract regarding capabilities of reading
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadRepository<TId, TEntity> where TEntity : Entity<TId>
    {
        /// <summary>
        /// Gets an instance of the entity by is Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns>An entity that matches the Id</returns>
        Task<TEntity> Get(TId id, CancellationToken token);


        /// <summary>
        ///  Gets a list of entities in a page format ordered by a field and order
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetList<TOrderKey>(int take, int skip, Func<TEntity, TOrderKey> orderBy, int orderDirection, CancellationToken token);

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> os Entities
        /// </summary>
        /// <param name="CreatedBiggerThen">lower date</param>
        /// <param name="CreatedlessThen"> upper date</param>
        /// <param name="take">number of records to retrieve</param>
        /// <param name="skip">number of records to skip</param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetByTimeInterval(DateTime CreatedBiggerThen, DateTime CreatedlessThen, int take, int skip, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get entities by interval created date and tags
        /// </summary>
        /// <param name="containingTags"></param>
        /// <param name="CreatedBiggerThen"></param>
        /// <param name="CreatedlessThen"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetByTagsTimeInterval(IEnumerable<string> containingTags, DateTime CreatedBiggerThen, DateTime CreatedlessThen, int take, int skip, CancellationToken token = default);

        /// <summary>
        ///  Get all entities that have the folowing tags
        /// </summary>
        /// <param name="containingTags"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetByTags(IEnumerable<string> containingTags, int take, int skip, CancellationToken token);


    }
}
