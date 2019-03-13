////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: ListController.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Controller to provide user with actions on their Lists
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhereToDo.Entities;
using WhereToDo.Infrastructure;
using WhereToDo.Models;
using WhereToDo.Services;

namespace WhereToDo.Controllers
{
    [Route("/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly PagingOptions _defaultPagingOptions;
        private readonly IListService _listService;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authzService;

        public ListController(
            IOptions<PagingOptions> defaultPagingOptions,
            IListService listService,
            IUserService userService,
            IAuthorizationService authorizationService)
        {
            _defaultPagingOptions = defaultPagingOptions.Value;
            _listService = listService;
            _userService = userService;
            _authzService = authorizationService;
        }

        // Return all lists for the user hitting this route. If user is not authenticated returns NotFound.
        [Authorize]
        [HttpGet(Name = nameof(GetListsAsync))]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "offset", "limit", "orderBy", "search"})]
        public async Task<IActionResult> GetListsAsync(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<List, ListEntity> sortOptions,
            [FromQuery] SearchOptions<List, ListEntity> searchOptions,
            CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState)); // BadRequest if Model is invalid

            // Get options from URL or provide default
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            if (User.Identity.IsAuthenticated)
            {
                // Can add policy logic for returning User Lists vs all Lists for an Admin.

                var currentUserId = await _userService.GetUserIdAsync(User);
             
                var lists = await _listService.GetUserListsAsync(
                    currentUserId,
                    pagingOptions,
                    sortOptions,
                    searchOptions,
                    ct);

                var collection = PagedCollection<List>.Create<ListResponse>(
                    Link.ToCollection(nameof(GetListsAsync)),
                    lists.Items.ToArray(),
                    lists.TotalSize,
                    pagingOptions);

                collection.ListsQuery = FormMetadata.FromResource<List>(
                    Link.ToForm(
                        nameof(GetListsAsync),
                        null,
                        Link.GetMethod,
                        Form.QueryRelation));

                return Ok(collection);
            }
            else // User Not Authenticated
                return NotFound();
        }

        // Returns a specific list based off its ID
        [Authorize]
        [HttpGet("{listId}", Name = (nameof(GetListByIdAsync)))]
        public async Task<IActionResult> GetListByIdAsync(int listId, CancellationToken ct)
        {
            var list = await _listService.GetListAsync(listId, ct);
            if (list == null) return NotFound();
            return Ok(list);
        }

        // Allows for a POST of a list from an authenticated user
        [Authorize]
        [HttpPost(Name = nameof(SubmitListAsync))]
        public async Task<IActionResult> SubmitListAsync(
            [FromBody] ListForm listForm,
            CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            int? currentUserId = await _userService.GetUserIdAsync(User);

            var listId = await _listService.CreateListAsync(currentUserId, listForm, ct);

            return Created(
                Url.Link(nameof(ListController.GetListByIdAsync),
                new { listId }),
                null);
        }

        // Allows for a DELETE of a specific list based off its ID
        [Authorize]
        [HttpDelete("{listId}", Name = nameof(DeleteListByIdAsync))]
        public async Task<IActionResult> DeleteListByIdAsync(
            int listId,
            CancellationToken ct)
        {
            await _listService.DeleteListAsync(listId, ct);
            return NoContent();
        }

        // Allows for a PATCH using a JsonPatchDocument for any list and any attribute based off its ID
        [Authorize]
        [HttpPatch("{listId}")]
        public async Task<IActionResult> PatchListAsync(
            [FromBody] JsonPatchDocument<List> patchDocument,
            int listId,
            CancellationToken ct)
        {
            if(patchDocument == null)
            {
                return BadRequest();
            }

            // Get List from DB based on ID
            var list = await _listService.GetListAsync(listId, ct);
            if (list == null) return NotFound();

            // Apply changes to List object
            patchDocument.ApplyTo(list);

            await _listService.UpdateListAsync(list, listId, ct);

            return Ok(list);
        }

    }
}
