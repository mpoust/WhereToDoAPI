////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: UserRoleEntity.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Represents the AspNetRole entity in the DB Context. No values are supplied except specifying the ID type is int since
 * IdentityRole handles everything that is needed.
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
    public class UserRoleEntity : IdentityRole<int>
    {
        public UserRoleEntity()
            : base()
        {
        }

        public UserRoleEntity(string roleName)
            : base(roleName)
        {
        }
    }
}
