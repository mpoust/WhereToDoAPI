////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: SortTerm.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Term applied to sort in API URL. Provided by referenced Lynda.com course
 * 
 * No sorting yet implemented - 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace WhereToDo.Infrastructure
{
    public class SortTerm
    {
        public string Name { get; set; }

        public string EntityName { get; set; }

        public bool Descending { get; set; }

        public bool Default { get; set; }
    }
}
