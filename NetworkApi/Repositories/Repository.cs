using NetWorkApi.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Bson;
using System.Globalization;

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


        /// <summary>
        /// Build the query
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        private FilterDefinition<Entity> BuildQuery(Dictionary<string, string> filters = null)
        {
            var filterBuilder = Builders<Entity>.Filter;
            var queryFilter = filterBuilder.Empty;

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    queryFilter = filterBuilder.And(
                        queryFilter,
                        filterBuilder.Regex(
                            filter.Key.First().ToString().ToUpper() + filter.Key.Substring(1),
                            new BsonRegularExpression("/.*" + filter.Value + ".*/")
                        )
                    );
                }
            }

            return queryFilter;
        }


        /// <summary>
        /// Count item 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public int Count(Dictionary<string, string> filters = null)
        {
            return (int) collection.Count(BuildQuery(filters));
        }

        /// <summary>
        /// Query data
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="sortBy"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="isAccending"></param>
        /// <returns></returns>
        public IList<Entity> Find(Dictionary<string, string> filters = null, 
            int start = 0, 
            int limit = 10,
            string sortBy = null, 
            bool isAccending = true)
        {

            var cursor = collection.Find(BuildQuery(filters));

            if (sortBy != null)
            {
                var sort = Builders<Entity>.Sort;
                var sortDefinition = isAccending ? sort.Ascending(sortBy) : sort.Descending(sortBy);
                cursor.Sort(sortDefinition);
            }

            return cursor.Skip(start).Limit(limit).ToList();
        }
    }
}
