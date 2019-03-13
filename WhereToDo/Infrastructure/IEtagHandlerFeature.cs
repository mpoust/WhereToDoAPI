////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: IEtagHandlerFeature.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Interface as a part of a collection providing for ETagging. Provided by referenced Lynda.com course.
 * 
 * ETag not currently implemented 2-28-19
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


namespace WhereToDo.Infrastructure
{
    public interface IEtagHandlerFeature
    {
        bool NoneMatch(IEtaggable entity);
    }
}
