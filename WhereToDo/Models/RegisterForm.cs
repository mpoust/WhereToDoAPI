////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: RegisterForm.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Form for registering a user. It is assumed the password is validated on the front end.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhereToDo.Infrastructure;

namespace WhereToDo.Models
{
    public class RegisterForm
    {
        [Required]
        [Display(Name = "username", Description = "Username")]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        [Display(Name = "password", Description = "Password")]
        [Secret]
        public string Password { get; set; }
    }
}
