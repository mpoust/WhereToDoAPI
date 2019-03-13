////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: PagingOptions.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Options for paging the responses from the API. Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel.DataAnnotations;

namespace WhereToDo.Models
{
    public class PagingOptions
    {
        [Range(1, 9999, ErrorMessage = "Offset must be greater than 0.")]
        public int? Offset { get; set; }

        [Range(1, 100, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
        public int? Limit { get; set; }

        public PagingOptions Replace(PagingOptions newer)
        {
            return new PagingOptions
            {
                Offset = newer.Offset ?? Offset,
                Limit = newer.Limit ?? Limit
            };
        }
    }
}
