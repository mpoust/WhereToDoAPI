////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: ListResponse.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents a response by the API for lists. Logic provided by referenced Lynda.com course
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace WhereToDo.Models
{
    public class ListResponse : PagedCollection<List>
    {
        public Form ListsQuery { get; set; }
    }
}
