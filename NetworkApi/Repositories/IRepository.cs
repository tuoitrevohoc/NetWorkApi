using NetWorkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWorkApi.Repositories
{

    /// <summary>
    /// 
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
    }
}
