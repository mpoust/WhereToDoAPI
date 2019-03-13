////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: LinkRewriter.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Used with the filter to help rewriting links between API endpoints. Provided by referenced Lynda.com course
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Mvc;
using WhereToDo.Models;

namespace WhereToDo.Infrastructure
{
    public class LinkRewriter
    {
        private readonly IUrlHelper _urlHelper;

        public LinkRewriter(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        // This needs to be called right before a response is serialized and sent back to the client - achieved with a ResultFilter
        //  called LinkRewritingFilter
        public Link Rewrite(Link original)
        {
            if (original == null) return null;

            return new Link
            {
                Href = _urlHelper.Link(original.RouteName, original.RouteValues),
                Method = original.Method,
                Relations = original.Relations
            };
        }
    }
}
