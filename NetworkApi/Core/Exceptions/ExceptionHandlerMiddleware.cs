using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetWorkApi.Core.Exceptions
{

    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandlerMiddleware
    {

        /// <summary>
        /// the next request delegate
        /// </summary>
        private readonly RequestDelegate next;


        /// <summary>
        /// create an exception middle ware
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invoke the request 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ValidationException exception)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { "Error", exception.Message },
                        { "Detail", exception.Errors }      
                    })
                );
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(exception.Message)
                );
            }
        }


    }
}
