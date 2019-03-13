////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: ApplicationInfo.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents a model of static information for the application.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToDo.Infrastructure;

namespace WhereToDo.Models
{
    public class ApplicationInfo : Resource, IEtaggable
    {
        public string Name { get; set; }

        public string Tagline { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string Phone { get; set; }

        public Address Location { get; set; }

        // Not implemented
        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }

    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}
