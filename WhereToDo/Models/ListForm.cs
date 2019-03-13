////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: ListForm.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents the form that is supplied to the API with a POST when submitting a list by a user.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace WhereToDo.Models
{
    public class ListForm
    {
        public string Title { get; set; }

        public string Location { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

    }
}
