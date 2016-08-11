using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWorkApi.Core.Exceptions
{

    /// <summary>
    /// Validation Exception
    /// </summary>
    public class ValidationException: Exception
    {

        /// <summary>
        /// List of errors
        /// </summary>
        public IList<string> Errors;

        /// <summary>
        /// Create a validation exception with a model state dictionary
        /// </summary>
        /// <param name="modelState"></param>
        public ValidationException(ModelStateDictionary modelState)
            :base("Validation Error")
        {
            Errors = modelState.Values.SelectMany(item => item.Errors)
                                .Select(item => item.ErrorMessage)
                                .ToList();
        }

    }
}
