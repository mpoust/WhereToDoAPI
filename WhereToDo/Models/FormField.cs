////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: FormField.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Fields that may be processed in the form and their attributes. Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Newtonsoft.Json;
using System.ComponentModel;

namespace WhereToDo.Models
{
    public class FormField
    {
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FormFieldOption[] Options { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Pattern { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Required { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Secret { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(DefaultType)]
        public string Type { get; set; } = DefaultType;
        public const string DefaultType = "string";

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MinLength { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxLength { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }
    }
}
