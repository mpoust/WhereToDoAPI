////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: UserEntity.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents the AspNetUser entity in the DB Context. No values are supplied except specifying the ID type is int since
 * IdentityUser handles everything that is needed.
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToDo.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        // IdentityUser handles Username and Password
    }
}
