using Microsoft.AspNetCore.Mvc;
using NetWorkApi.Core.Model;
using NetWorkApi.Models;
using NetWorkApi.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace NetWorkApi.Controllers
{

    /// <summary>
    /// The data controller
    /// </summary>
    public class ResourceController<Entity>: Controller where Entity: AppModel
    {

        /// <summary>
        /// the repository
        /// </summary>
        protected IRepository<Entity> repository; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public ResourceController(IRepository<Entity> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Id of the model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Entity GetData(string id)
        {
            return repository.Get(id);
        }


        /// <summary>
        /// Query data from object
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpGet]
        public PagingData<Entity> Query(
            [FromQuery] string query = null,
            [FromQuery] int start = 0,
            [FromQuery] int limit = 10,
            [FromQuery] string sortBy = null,
            [FromQuery] bool isAccending = true
            )
        {
            Dictionary<string, string> filters = null;

            if (query != null)
            {
                filters = JsonConvert.DeserializeObject<Dictionary<string, string>>(query);
            }

            return new PagingData<Entity>( 
                repository.Count(filters),
                repository.Find(
                    filters,
                    start,
                    limit,
                    sortBy,
                    isAccending
                )
            );
        }
    }
}
