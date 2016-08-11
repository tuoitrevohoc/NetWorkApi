using Microsoft.AspNetCore.Mvc;
using NetWorkApi.Core.Model;
using NetWorkApi.Core.Validations;
using NetWorkApi.Models;
using NetWorkApi.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /// Meta data for this entity
        /// </summary>
        protected List<ColumnData> metaData;

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

        /// <summary>
        /// Get validation constraints
        /// </summary>
        [HttpGet("metadata")]
        public IEnumerable<ColumnData> GetConstraints()
        {
            if (metaData == null)
            {
                metaData = new List<ColumnData>();

                var type = typeof(Entity);
                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var columnData = new ColumnData
                    {
                        Name = property.Name
                    };

                    var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();

                    if (displayAttribute != null)
                    {
                        columnData.DisplayName = displayAttribute.Name;
                        columnData.Description = displayAttribute.Description;
                    }

                    var attributes = property.GetCustomAttributes<Attribute>();
                    var validations = new List<DataValidation>();

                    foreach (var attribute in attributes)
                    {
                        var emailAddressAttribute = attribute as EmailAddressAttribute;

                        if (emailAddressAttribute != null)
                        {
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

                    columnData.Validations = validations;
                    metaData.Add(columnData);
                }
            }

            return metaData;
        }
    }
}
