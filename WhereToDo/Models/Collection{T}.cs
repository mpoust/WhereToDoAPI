////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: Collection.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents a collection of model resources returned by the API. Provided by referenced Lynda.com course
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToDo.Models
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
