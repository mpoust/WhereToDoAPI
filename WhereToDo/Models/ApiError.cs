////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: ApiError.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Returns errors processed by requests to the API. Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq;

namespace WhereToDo.Models
{
    public class ApiError
    {
        public ApiError() { }

        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            Message = "Invalid parameters";
            Detail = modelState
                .FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors
                .FirstOrDefault().ErrorMessage;
        }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string StackTrace { get; set; } // Development only TODO: REMOVE
    }
}
