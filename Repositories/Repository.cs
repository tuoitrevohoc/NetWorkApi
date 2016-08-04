using NetWorkApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace NetWorkApi.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public class Repository<Entity> : IRepository<Entity> where Entity : AppModel
    {

        /// <summary>
        /// The database set
        /// </summary>
        private DbSet<Entity> dbSet;

        /// <summary>
        /// The database context
        /// </summary>
        private DbContext context;


        /// <summary>
        /// Set for linq query
        /// </summary>
        public IEnumerable<Entity> Elements
        {
            get
            {
                return dbSet;
            }
        }

        /// <summary>
        /// Initialize with a database set
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            dbSet = context.Set<Entity>();
            this.context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(Entity entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();

            return true;
        }


        /// <summary>
        /// Get an element by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity Get(int id)
        {
            return dbSet.FirstOrDefaultAsync(item => item.Id == id).Result;
        }


        /// <summary>
        /// Save an item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Save(Entity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();

            return entity;
        }
    }
}
