////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: DefaultUserService.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Defines actions that are implemented by the UsersController
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WhereToDo.Entities;
using WhereToDo.Models;

namespace WhereToDo.Services
{
    public class DefaultUserService : IUserService
    {
        private readonly WTD_DBContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DefaultUserService(
            UserManager<UserEntity> userManager,
            IConfigurationProvider mappingConfiguration)
        {
            _userManager = userManager;
            _mappingConfiguration = mappingConfiguration;
        }

        // Creates a user with the given RegisterForm
        public async Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterForm form)
        {
            // Make user entity - TODO: add more attributes to a user
            var entity = new UserEntity
            {
                UserName = form.UserName,
            };

            // Need to add salt to password or is it applied automatically?
            var result = await _userManager.CreateAsync(entity, form.Password); // Validates password here            

            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault()?.Description;
                return (false, firstError); // Error of user creation
            }
            else // User successfully created
            {
                // Add newly created user to a role
                var result2 = await _userManager.AddToRoleAsync(entity, "Employee"); // All created accounts added to 'Employee' role
                if (!result.Succeeded)
                {
                    var error = result2.Errors.FirstOrDefault()?.Description;
                    return (false, error);
                }
                return (true, null);
            }
        }

        // Gets the current user based off the authenticated token supplied with request header
        public async Task<User> GetUserAsync(ClaimsPrincipal user)
        {
            var entity = await _userManager.GetUserAsync(user);
            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<User>(entity);
        }

        // Gets the current userID based off the authenticated token supplied with request header
        public async Task<int?> GetUserIdAsync(ClaimsPrincipal principal)
        {
            var entity = await _userManager.GetUserAsync(principal);
            if (entity == null) return null;

            return entity.Id;
        }

        // Returns all users - restricted to admins in UserContoller - not currently implemented 2-28-19
        public async Task<PagedResults<User>> GetUsersAsync(
            PagingOptions pagingOptions,
            SortOptions<User, UserEntity> sortOptions,
            SearchOptions<User, UserEntity> searchOptions)
        {
            IQueryable<UserEntity> query = _userManager.Users;
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<User>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<User>
            {
                Items = items,
                TotalSize = size
            };
        }

        // Returns a user based off the supplied userID
        public async Task<User> GetUserByIdAsync(int userId)
        {
            var entity = await _userManager.Users
                .SingleOrDefaultAsync(x => x.Id == userId);
            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<User>(entity);
        }

        // Updates a user's password with the password form. Updates for the current user based off authenticated token supplied with request header
        public async Task<(bool Succeeded, string ErrorMessage)> UpdatePasswordAsync(
            ClaimsPrincipal principal,
            PasswordUpdateForm form)
        {
            var entity = await _userManager.GetUserAsync(principal);

            var result = await _userManager.ChangePasswordAsync(entity, form.CurrentPassword, form.NewPassword);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault()?.Description;
                return (false, firstError); // Password update error
            }
            else // Password successfully changed
            {
                return (true, null); // No errors to report.
            }
        }
    }
}
