////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: JsonExceptionFilter.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Exceptions converted to JSON - provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WhereToDo.Models;

namespace WhereToDo.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env; // Used to detect if in development, staging, or production

        public JsonExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Runs anytime there is an unhandled exception within the API. Creates new instance of ApiError class and
        ///  serializes it to return to the client as a JSON object with status code of 500.
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.TargetSite.Name;
                error.StackTrace = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error has occured.";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
