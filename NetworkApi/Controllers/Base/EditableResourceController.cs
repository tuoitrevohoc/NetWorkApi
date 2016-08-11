using Microsoft.AspNetCore.Mvc;
using NetWorkApi.Core.Exceptions;
using NetWorkApi.Core.Validations;
using NetWorkApi.Models;
using NetWorkApi.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
        /// Get validation constraints
        /// </summary>
        [HttpGet("metadata")]
        public Dictionary<string, IEnumerable<DataValidation>> GetConstraints()
        {
            var result = new Dictionary<string, IEnumerable<DataValidation>>();
            var type = typeof(Entity);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes<Attribute>();
                var validations = new List<DataValidation>();

                foreach (var attribute in attributes)
                {
                    var emailAddressAttribute = attribute as EmailAddressAttribute;

                    if (emailAddressAttribute != null) {
                        validations.Add(new RegularExpressionValidation(
                            "^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,63}$",
                            emailAddressAttribute.ErrorMessage
                            ));
                        break;
                    }

                    var rangeAttribute = attribute as RangeAttribute;
                    if (rangeAttribute != null)
                    {
                        validations.Add(new RangeValidation(
                            rangeAttribute.Minimum, 
                            rangeAttribute.Maximum, 
                            rangeAttribute.ErrorMessage));
                        break;
                    }

                    var stringLengthAttribute = attribute as StringLengthAttribute;
                    if (stringLengthAttribute != null)
                    {
                        validations.Add(new StringLengthValidation(
                                stringLengthAttribute.MinimumLength,
                                stringLengthAttribute.MaximumLength,
                                stringLengthAttribute.ErrorMessage
                            ));
                        break;
                    }

                }

                result.Add(property.Name, validations);
            }

            return result;
        }

        /// <summary>
        /// Create an entity
        /// </summary>
        /// <param name="entity"></param>
        [HttpPut]
        public Entity Create([FromBody] Entity entity)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

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
            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }

            return repository.Save(entity);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return repository.Delete(id);
        }
    }
}
