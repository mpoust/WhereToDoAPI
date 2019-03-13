////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: IListService.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Interface defining actions for the DefaultListService. Mapped in Startup.cs
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Threading;
using System.Threading.Tasks;
using WhereToDo.Entities;
using WhereToDo.Models;

namespace WhereToDo.Services
{
    public interface IListService
    {
        Task<PagedResults<List>> GetListsAsync(
            PagingOptions pagingOptions,
            SortOptions<List, ListEntity> sortOptions,
            SearchOptions<List, ListEntity> searchOptions,
            CancellationToken ct);

        Task<PagedResults<List>> GetUserListsAsync(
            int? userId,
            PagingOptions pagingOptions,
            SortOptions<List, ListEntity> sortOptions,
            SearchOptions<List, ListEntity> searchOptions,
            CancellationToken ct);

        Task<int> CreateListAsync(
            int? userId,
            ListForm form,
            CancellationToken ct);

        Task DeleteListAsync(int listId, CancellationToken ct);

        Task<List> GetListAsync(int listId, CancellationToken ct);

        Task UpdateListAsync(List list, int listId, CancellationToken ct);
    }
}
