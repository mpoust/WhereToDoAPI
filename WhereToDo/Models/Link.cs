////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: Link.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Base class representing Links between API resources. Provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Newtonsoft.Json;

namespace WhereToDo.Models
{
    public class Link
    {
        public const string GetMethod = "GET";
        public const string PostMethod = "POST";
        public const string DeleteMethod = "DELETE";

        // Static helper method to return a link with some default values
        public static Link To(
            string routeName,
            object routeValues = null)
            => new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = null
            };

        public static Link ToCollection(
            string routeName,
            object routeValues = null)
            => new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = new string[] { "collection" }
            };

        public static Link ToForm(
            string routeName,
            object routeValues = null,
            string method = PostMethod,
            params string[] relations)
            => new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = method,
                Relations = relations
            };

        [JsonProperty(Order = -4)]
        public string Href { get; set; }

        [JsonProperty(Order = -3,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Method { get; set; }

        [JsonProperty(Order = -2,
            PropertyName = "rel",
            NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }

        [JsonIgnore]
        public string RouteName { get; set; } // Stores route name before being rewritten -- only used internally, not serialized

        [JsonIgnore]
        public object RouteValues { get; set; } // Stores the route values before being rewritten -- only used internally, not serialized
    }
}
