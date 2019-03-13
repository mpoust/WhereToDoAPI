////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: IUserService.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Interface defining actions for the DefaultUserService. Mapped in Startup.cs
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Security.Claims;
using System.Threading.Tasks;
using WhereToDo.Entities;
using WhereToDo.Models;

namespace WhereToDo.Services
{
    public interface IUserService
    {
        Task<PagedResults<User>> GetUsersAsync(
            PagingOptions pagingOptions,
            SortOptions<User, UserEntity> sortOptions,
            SearchOptions<User, UserEntity> searchOptions);

        Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterForm form);

        Task<int?> GetUserIdAsync(ClaimsPrincipal principal);

        Task<User> GetUserByIdAsync(int userId);

        Task<User> GetUserAsync(ClaimsPrincipal user);

        Task<(bool Succeeded, string ErrorMessage)> UpdatePasswordAsync(
            ClaimsPrincipal principal,
            PasswordUpdateForm form);
    }
}
