////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: HttpRequestExtensions.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Provided by the referenced Lynda.com course. Extending HTTP Request methods.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToDo.Infrastructure;

namespace WhereToDo.Extensions
{
    public static class HttpRequestExtensions
    {
        public static IEtagHandlerFeature GetEtagHandler(this HttpRequest request)
                    => request.HttpContext.Features.Get<IEtagHandlerFeature>();
    }
}
