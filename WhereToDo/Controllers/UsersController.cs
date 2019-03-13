////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: UsersController.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Controller that provides all user actions, from creating to updating passwords.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToDo.Entities;
using WhereToDo.Models;
using WhereToDo.Services;

namespace WhereToDo.Controllers
{
    [Route("/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly PagingOptions _defaultPagingOptions;
        private readonly IAuthorizationService _authzService;

        public UsersController(
            IUserService userService,
            IOptions<PagingOptions> defaultPagingOptions,
            IAuthorizationService authorizationService)
        {
            _userService = userService;
            _defaultPagingOptions = defaultPagingOptions.Value;
            _authzService = authorizationService;
        }

        // Returns all users if an admin (not implemented) or just the current user and their information
        [Authorize]
        [HttpGet(Name = nameof(GetVisibleUsers))]
        public async Task<ActionResult<PagedCollection<User>>> GetVisibleUsers(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<User, UserEntity> sortOptions,
            [FromQuery] SearchOptions<User, UserEntity> searchOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var users = new PagedResults<User>()
            {
                Items = Enumerable.Empty<User>()
            };

            if (User.Identity.IsAuthenticated)
            { 
                var canSeeEveryone = await _authzService.AuthorizeAsync(
                    User, "ViewAllUsersPolicy");
                if (canSeeEveryone.Succeeded)
                {
                    // Executive, view everyone                
                    users = await _userService.GetUsersAsync(
                        pagingOptions, sortOptions, searchOptions);
                }
                else // Only return self
                {
                    var myself = await _userService.GetUserAsync(User);
                    users.Items = new[] { myself };
                    users.TotalSize = 1;
                }
            }

            var collection = PagedCollection<User>.Create<UsersResponse>(
                Link.ToCollection(nameof(GetVisibleUsers)),
                users.Items.ToArray(),
                users.TotalSize,
                pagingOptions);

            collection.Me = Link.To(nameof(UserinfoController.Userinfo));

            // Register user info
            /*
            collection.Register = FormMetadata.FromModel(
                new RegisterForm(),
                Link.ToForm(nameof(RegisterUser), relations: Form.CreateRelation));
            */

            return collection;
        }

        // Returns a specific user by their ID (only returns other users if an admin - not implemented)
        [Authorize]
        [HttpGet("{userId}", Name = nameof(GetUserById))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            var currentUserId = await _userService.GetUserIdAsync(User);
            if (currentUserId == null) return NotFound();

            if (currentUserId == userId)
            {
                var myself = await _userService.GetUserAsync(User);
                return myself;
            }

            var canSeeEveryone = await _authzService.AuthorizeAsync(
                User, "ViewAllUsersPolicy");
            if (!canSeeEveryone.Succeeded) return NotFound();

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            return user;
        }

        // Allows for a POST of a user to create an register a new user in the DB
        [HttpPost(Name = nameof(RegisterUser))]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> RegisterUser(
            [FromBody] RegisterForm form)
        {
            var (succeeded, message) = await _userService.CreateUserAsync(form);

            if (succeeded) return Created(
                Url.Link(nameof(UserinfoController.Userinfo), null),
                null);

            return BadRequest(new ApiError
            {
                Message = "Registration Failed.",
                Detail = message
            });
        }


        // Allows for a PATCH only to the user's password. Uses .NET Identity UpdatePasswordAsync
        [Authorize]
        [HttpPatch(Name = nameof(ChangeUserPassword))]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ChangeUserPassword(
            [FromBody] PasswordUpdateForm form)
        {
            var (succeeded, message) = await _userService.UpdatePasswordAsync(User, form);

            if (succeeded) return Ok();

            return BadRequest(new ApiError
            {
                Message = "Update Password Failed.",
                Detail = message
            });
        }
    }
}
