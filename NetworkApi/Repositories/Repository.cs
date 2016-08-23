using NetWorkApi.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Bson;
using NetworkApi.Repositories.Query;

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
        private FilterDefinition<Entity> BuildQuery(DataQuery query = null)
        {
            var filterBuilder = Builders<Entity>.Filter;
            var queryFilter = filterBuilder.Empty;

            if (query != null)
            {
                queryFilter = BuildFilter(filterBuilder, query.Condition);
            }

            return queryFilter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterBuilder"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private FilterDefinition<Entity> BuildFilter(FilterDefinitionBuilder<Entity> filterBuilder,
            Condition condition)
        {
            var queryFilter = filterBuilder.Empty;

            if (condition is LogicCondition) {
                var logicCondition = condition as LogicCondition;
                var filters = new List<FilterDefinition<Entity>>();

                foreach (var filterCondition in logicCondition.Conditions) {
                    var filter = BuildFilter(filterBuilder, filterCondition);
                    filters.Add(filter);
                }

                if (logicCondition.Operator == LogicOperator.And) {
                    queryFilter = filterBuilder.And(filters);
                } else {
                    queryFilter = filterBuilder.Or(filters);
                }
            } else {
                var fieldCondition = condition as ExpressionCondition;
                switch (fieldCondition.Operator) {
                    case Operator.Greater:
                        queryFilter = filterBuilder.Gt(fieldCondition.FieldName, fieldCondition.Value);
                        break;
                    case Operator.Less:
                        queryFilter = filterBuilder.Lt(fieldCondition.FieldName, fieldCondition.Value);
                        break;
                    case Operator.GreaterOrEquals:
                        queryFilter = filterBuilder.Gte(fieldCondition.FieldName, fieldCondition.Value);
                        break;
                    case Operator.LessOrEquals:
                        queryFilter = filterBuilder.Lte(fieldCondition.FieldName, fieldCondition.Value);
                        break;
                    default:
                        queryFilter = filterBuilder.Eq(fieldCondition.FieldName, fieldCondition.Value);
                        break;
                }
            }

            return queryFilter;
        }



        /// <summary>
        /// Count item 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int Count(DataQuery query = null)
        {
            return (int) collection.Count(BuildQuery(query));
        }

        /// <summary>
        /// Query data
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sortBy"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="isAccending"></param>
        /// <returns></returns>
        public IList<Entity> Find(DataQuery query = null, 
            int start = 0, 
            int limit = 10,
            string sortBy = null, 
            bool isAccending = true)
        {

            var cursor = collection.Find(BuildQuery(query));

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
