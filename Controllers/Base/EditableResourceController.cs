using Microsoft.AspNetCore.Mvc;
using NetWorkApi.Models;
using NetWorkApi.Repositories;

namespace NetWorkApi.Controllers.Base
{


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class EditableResourceController<Entity>: ResourceController<Entity>
        where Entity: AppModel
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public EditableResourceController(IRepository<Entity> repository): base(repository)
        {
        }

        /// <summary>
        /// Create an entity
        /// </summary>
        /// <param name="entity"></param>
        [HttpPut]
        public Entity Create([FromBody] Entity entity)
        {
            return repository.Save(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public Entity Save([FromBody] Entity entity)
        {
            return repository.Save(entity);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(int id)
        {
            return true;
        }
    }
}
