using NetWorkApi.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Bson;

namespace NetWorkApi.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public class Repository<Entity> : IRepository<Entity> where Entity : AppModel
    {

        /// <summary>
        /// the collection
        /// </summary>
        private IMongoCollection<Entity> collection;


        /// <summary>
        /// Set for linq query
        /// </summary>
        public IEnumerable<Entity> Elements
        {
            get
            {
                return collection.AsQueryable();
            }
        }

        /// <summary>
        /// Initialize with a database set
        /// </summary>
        /// <param name="collection"></param>
        public Repository(IMongoCollection<Entity> collection)
        {
            this.collection = collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            var result = collection.DeleteOne(item => item.Id.Equals(id));

            return result.DeletedCount == 1;
        }


        /// <summary>
        /// Get an element by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity Get(string id)
        {
            return Elements.FirstOrDefault(item => item.Id.Equals(id));
        }


        /// <summary>
        /// Save an item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Save(Entity entity)
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
                collection.InsertOne(entity);
            } else
            {
                collection.ReplaceOne(item => item.Id == entity.Id, entity);
            }

            return entity;
        }
    }
}
