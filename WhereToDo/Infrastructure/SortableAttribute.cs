////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: SortableAttribute.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Attribute that can be applied to models to sort responses from the API. Provided by referenced Lynda.com course.
 * 
 * No sorting yet implemented - 2-28-19
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
    public class SortableAttribute : Attribute
    {
        public string EntityProperty { get; set; }

        public bool Default { get; set; }
    }
}
