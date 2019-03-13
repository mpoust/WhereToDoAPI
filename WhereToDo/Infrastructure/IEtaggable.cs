////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: IEtaggable.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Interface as a part of a collection providing for ETagging. Provided by referenced Lynda.com course.
 * 
 * ETAg not currently implemented 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToDo.Infrastructure
{
    public interface IEtaggable
    {
        string GetEtag();
    }
}
