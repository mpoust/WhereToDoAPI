////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: NotesEntity.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents the Notes in the DB Context - Not currently implemented
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
    public class NotesEntity
    {
        public int Id { get; set; }

        public string Note { get; set; }

        public byte Status { get; set; }
    }
}
