////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: EtagHeaderFiler.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Part of a collection of classes providing for ETagging. Provided by referenced Lynda.com course.
 * 
 * ETag not currently implemented. 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using WhereToDo.Infrastructure;

namespace WhereToDo.Filters
{
    public class EtagHeaderFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Features.Set<IEtagHandlerFeature>(
                new EtagHandlerFeature(context.HttpContext.Request.Headers));

            var executed = await next();

            var result = executed?.Result as ObjectResult;

            var etag = (result?.Value as IEtaggable)?.GetEtag();
            if (string.IsNullOrEmpty(etag)) return;

            if (!etag.Contains('"'))
            {
                etag = $"\"{etag}\"";
            }

            context.HttpContext.Response.Headers.Add("ETag", etag);

            // If a response body was set so that we would add
            // the ETag header, but the status code is 304,
            // the body should be removed.
            if (result.StatusCode == 304)
            {
                result.Value = null;
            }

            return;
        }
    }
}
