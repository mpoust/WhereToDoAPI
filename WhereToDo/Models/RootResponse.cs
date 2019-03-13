////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: RootResponse.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Base response provided by the RootController. Logic provided by referenced Lynda.com course
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


namespace WhereToDo.Models
{
    public class RootResponse : Resource
    {
        public Link Info { get; set; } // Static Info about application for testing

        public Link Users { get; set; }

        public Link UserInfo { get; set; }

        public Link List { get; set; }

        public Link Token { get; set; }
    }
}
