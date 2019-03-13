////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: FormFieldOption.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Options for the form fields. Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Newtonsoft.Json;

namespace WhereToDo.Models
{
    public class FormFieldOption
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        public object Value { get; set; }
    }
}
