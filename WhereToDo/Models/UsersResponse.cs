////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: UsersResponse.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Response to the Users route. Provided by referenced Lynda.com course
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
    public class UsersResponse : PagedCollection<User>
    {
        public Form Register { get; set; }

        public Link Me { get; set; }
    }
}
