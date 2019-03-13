////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: Form.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Base class for forms provided to API. Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace WhereToDo.Models
{
    public class Form : Collection<FormField>
    {
        public const string Relation = "form";
        public const string EditRelation = "edit-form";
        public const string CreateRelation = "create-form";
        public const string QueryRelation = "query-form";
    }
}
