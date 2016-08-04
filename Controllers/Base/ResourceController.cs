using Microsoft.AspNetCore.Mvc;
using NetWorkApi.Models;
using NetWorkApi.Repositories;
using System.Collections.Generic;

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
        public Entity GetData(int id)
        {
            return repository.Get(id);
        }


        /// <summary>
        /// Get a list of data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<Entity> GetList()
        {
            return null;
        }
    }
}
