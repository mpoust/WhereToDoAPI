////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: PasswordUpdateForm.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Form for updating a user's password. It is assumed the NewPassword is validated on the front-end before sending the request.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel.DataAnnotations;
using WhereToDo.Infrastructure;

namespace WhereToDo.Models
{
    public class PasswordUpdateForm
    {
        [Required]
        [Display(Name = "current password", Description = "New Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "new password", Description = "New Password")]
        [Secret]
        public string NewPassword { get; set; }
    }
}
