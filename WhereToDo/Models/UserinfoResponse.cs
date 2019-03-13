////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: UserinfoResponse.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents response when userinfo route is hit.  Provided by referenced Lynda.com course
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using AspNet.Security.OpenIdConnect.Primitives;
using Newtonsoft.Json;

namespace WhereToDo.Models
{
    public class UserinfoResponse : Resource
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = OpenIdConnectConstants.Claims.Subject)]
        public string Subject { get; set; }


        // Not used
        [JsonProperty(PropertyName = OpenIdConnectConstants.Claims.GivenName)]
        public string GivenName { get; set; }

        // Not used
        [JsonProperty(PropertyName = OpenIdConnectConstants.Claims.FamilyName)]
        public string FamilyName { get; set; }
    }
}
