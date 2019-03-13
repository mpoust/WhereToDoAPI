////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: UserinfoController.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Controller that returns information for the user hitting the route. Logic provided by referenced Lynda.com course.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhereToDo.Models;
using WhereToDo.Services;

namespace WhereToDo.Controllers
{
    [Route("/[controller]")]
    [Authorize]
    [ApiController]
    public class UserinfoController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserinfoController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /userinfo
        [HttpGet(Name = nameof(Userinfo))]
        [ProducesResponseType(401)]
        public async Task<ActionResult<UserinfoResponse>> Userinfo()
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The user does not exist."
                });
            }

            var userId = await _userService.GetUserIdAsync(User);

            return new UserinfoResponse
            {
                Id = (int)userId,
                Self = Link.To(nameof(Userinfo)),
                Subject = Url.Link(
                    nameof(UsersController.GetUserById),
                    new { userId })
            };
        }
    }
}
