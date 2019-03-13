////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: ListEntity.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents the List in the DB Context
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToDo.Entities
{
    public class ListEntity
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public byte Status { get; set; }
    }
}
