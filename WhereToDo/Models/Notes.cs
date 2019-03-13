////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: Notes.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents the Notes resource for use within the API. Corresponds with NotesEntity.
 * 
 * Notes not currently implemented - 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace WhereToDo.Models
{
    public class Notes : Resource
    {
        public string Note { get; set; }

        public byte Status { get; set; }
    }
}
