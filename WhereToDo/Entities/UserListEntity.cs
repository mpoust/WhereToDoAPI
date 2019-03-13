////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: UserListEntity.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents the User_List entity in the DB Context.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToDo.Entities
{
    public class User_ListEntity
    {
        public int UserId { get; set; }

        public int ListId { get; set; }
    }
}
