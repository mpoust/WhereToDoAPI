////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: SearchableAttribute.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Attribute that can be applied to models to search in API URL. Provided by referenced Lynda.com course.
 * 
 * No searching yet implemented - 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;

namespace WhereToDo.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableAttribute : Attribute
    {
        public ISearchExpressionProvider ExpressionProvider { get; set; }
            = new DefaultSearchExpressionProvider();
    }
}
