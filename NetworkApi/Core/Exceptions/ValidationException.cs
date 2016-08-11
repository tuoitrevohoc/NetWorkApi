using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetWorkApi.Core.Exceptions
{

    /// <summary>
    /// Validation Exception
    /// </summary>
    public class InvalidModelStateException: Exception
    {

        /// <summary>
        /// List of errors
        /// </summary>
        public IList<string> Errors;

        /// <summary>
        /// Create a validation exception with a model state dictionary
        /// </summary>
        /// <param name="modelState"></param>
        public InvalidModelStateException(ModelStateDictionary modelState)
            :base("Validation Error")
        {
            Errors = modelState.Values.SelectMany(item => item.Errors)
                                .Select(item => item.ErrorMessage)
                                .ToList();
        }

    }
}
