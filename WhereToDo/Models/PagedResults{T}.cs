////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: PagedResults.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Provided by referenced Lynda.com course
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace WhereToDo.Models
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalSize { get; set; }
    }
}
