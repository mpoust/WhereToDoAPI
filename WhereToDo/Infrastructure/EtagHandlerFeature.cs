////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: EtagHandlerFeature.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Part of a collection of classes providing for ETagging. Provided by referenced Lynda.com course.
 * 
 * ETag not currently implemented. 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToDo.Infrastructure
{
    public class EtagHandlerFeature : IEtagHandlerFeature
    {
        private IHeaderDictionary _headers;

        public EtagHandlerFeature(IHeaderDictionary headers)
        {
            _headers = headers;
        }

        public bool NoneMatch(IEtaggable entity)
        {
            if (!_headers.TryGetValue("If-None-Match", out var etags)) return true;

            var entityEtag = entity.GetEtag();
            if (string.IsNullOrEmpty(entityEtag)) return true;

            if (!entityEtag.Contains('"'))
            {
                entityEtag = $"\"{entityEtag}\"";
            }

            return !etags.Contains(entityEtag);
        }
    }
}
