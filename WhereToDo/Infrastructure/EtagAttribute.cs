////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: EtagAttribute.cs
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

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToDo.Filters;

namespace WhereToDo.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EtagAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        => new EtagHeaderFilter();
    }
}
