using Microsoft.AspNetCore.Mvc;
using NetWorkApi.Models;
using NetWorkApi.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

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
        /// Get validation constraints
        /// </summary>
        [HttpGet("metadata")]
        public Dictionary<string, IEnumerable<Attribute>> GetConstraints()
        {
            var result = new Dictionary<string, IEnumerable<Attribute>>();
            var type = typeof(Entity);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes<Attribute>();
                result.Add(property.Name, attributes);
            }

            return result;
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
        /// Get a list of data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<Entity> GetList()
        {
            return repository.Elements.ToList();
        }
    }
}
