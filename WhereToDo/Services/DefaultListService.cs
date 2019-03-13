////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: DefaultListService.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Defines actions that are implemented by the ListController
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhereToDo.Entities;
using WhereToDo.Models;

namespace WhereToDo.Services
{
    public class DefaultListService : IListService
    {
        private readonly WTD_DBContext _context;

        public DefaultListService(
            WTD_DBContext context)
        {
            _context = context;
        }

        // Creates a list for a given userID with the supplied ListForm
        public async Task<int> CreateListAsync(int? userId, ListForm form, CancellationToken ct)
        {
            // Create Entity for DB entry
            var newList = await _context.List.AddAsync(new ListEntity
            {
                Title = form.Title,
                Location = form.Location,
                Lat = form.Lat,
                Long = form.Long,
                Status = 0
            });

            // Attempt to save entity to the DB
            var created = await _context.SaveChangesAsync(ct);

            if (created < 1) throw new InvalidOperationException("Could not create the list.");

            // Add entry into User_List - connecting users with their lists
            var newUserList = _context.User_List.Add(new User_ListEntity
            {
                UserId = userId ?? default(int),
                ListId = newList.Entity.Id
            });

            // Attempt to save entity to the DB
            var created2 = await _context.SaveChangesAsync(ct);

            if (created2 < 1) throw new InvalidOperationException("Could not create the user list.");

            return newList.Entity.Id; // Return the ID of the newly created list.
        }

        // Deletes a given list by its listID
        public async Task DeleteListAsync(int listId, CancellationToken ct)
        {
            // Find the list by ID in the DB Context
            var list = await _context.List
                .SingleOrDefaultAsync(l => l.Id == listId, ct);
            if (list == null) return;

            // Delete User_List Record
            var user_list = await _context.User_List
                .SingleOrDefaultAsync(ul => ul.ListId == listId, ct);
            if (user_list == null) return;


            // Note: below has to be completed as such to allow for the DB to remove the dependent table entries. 
            //       moving to a stored procedure would increase efficiency.

            // Attempt to remove User_List entry and save the DB context
            _context.User_List.Remove(user_list); // TODO: update to stored procedure instead
            await _context.SaveChangesAsync();
            // Attempt to remove the List entry and save the DB context
            _context.List.Remove(list);
            await _context.SaveChangesAsync();
        }

        // Get a given list by its listID
        public async Task<List> GetListAsync(int listId, CancellationToken ct)
        {
            // Find the list
            var entity = await _context.List.SingleOrDefaultAsync(l => l.Id == listId, ct);
            if (entity == null) return null;
            return Mapper.Map<List>(entity);
        }

        // Returns all lists in the DB - Not currently implemented 2-28-19
        public Task<PagedResults<List>> GetListsAsync(
            PagingOptions pagingOptions,
            SortOptions<List, ListEntity> sortOptions,
            SearchOptions<List, ListEntity> searchOptions,
            CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        // Returns all user's lists based on userID and supplied paging, sort, and search options.
        public async Task<PagedResults<List>> GetUserListsAsync(
            int? userId,
            PagingOptions pagingOptions,
            SortOptions<List, ListEntity> sortOptions,
            SearchOptions<List, ListEntity> searchOptions,
            CancellationToken ct)
        {
            if (userId == null) return null;

            // Find all user lists from DB Context
            IQueryable<ListEntity> query = from l in _context.List
                                           join ul in _context.User_List
                                           on l.Id equals
                                           ul.ListId
                                           where ul.UserId == userId
                                           select l;

            // All lists returned by query
            var allLists = await query
                .ProjectTo<List>()
                .ToListAsync();

            // Cut to paged lists if there is any paging options (default applied otherwise)
            var pagedLists = allLists
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value);

            // Return collection of user lists with specified options applied.
            return new PagedResults<List>
            {
                Items = pagedLists,
                TotalSize = allLists.Count
            };
        }

        // Will be able to be used for both modifying list and updating the status of the list as a whole - used with a PATCH
        public async Task UpdateListAsync(List list, int listId, CancellationToken ct)
        {
            var entity = await _context.List.SingleOrDefaultAsync(l => l.Id == listId, ct);

            // Copy over values from modified list - will allow for PATCH to work on all properties of list
            entity.Title = list.Title;
            entity.Location = list.Location;
            entity.Lat = list.Lat;
            entity.Long = list.Long;
            entity.Status = list.Status;

            _context.List.Update(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
