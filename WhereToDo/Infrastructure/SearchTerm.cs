////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: SearchTerm.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Term applied to search in API URL. Provided by referenced Lynda.com course.
 * 
 * No searching yet implemented - 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace WhereToDo.Infrastructure
{
    public class SearchTerm
    {
        public string Name { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public bool ValidSyntax { get; set; }

        public ISearchExpressionProvider ExpressionProvider { get; set; }
    }
}
