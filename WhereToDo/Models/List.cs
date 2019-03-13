////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: List.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents the List resource for use within the API. Corresponds with the ListEntity
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace WhereToDo.Models
{
    public class List : Resource
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public byte Status { get; set; }

        public List<Notes> Notes { get; set; }
    }
}
