using NetWorkApi.Core.Model;
using NetWorkApi.Models;
using System.Collections.Generic;
using NetworkApi.Repositories.Query;

namespace NetWorkApi.Repositories
{

    /// <summary>
    /// The Interface for repository
    /// </summary>
    public interface IRepository<Entity> where Entity: AppModel
    {

        /// <summary>
        /// The dbSet
        /// </summary>
        IEnumerable<Entity> Elements { get; }

        /// <summary>
        /// Get an entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Entity Get(string id);

        /// <summary>
        /// Save an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Entity Save(Entity entity);

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);

        /// <summary>
        /// Count number of item 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int Count(DataQuery query = null);

        /// <summary>
        /// Query for data
        /// </summary>
        /// <param name="query"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="sortBy"></param>
        /// <param name="isAccending"></param>
        /// <returns></returns>
        IList<Entity> Find(DataQuery query = null,
            int start = 0,
            int limit = 10,
            string sortBy = null,
            bool isAccending = true);
    }
}
