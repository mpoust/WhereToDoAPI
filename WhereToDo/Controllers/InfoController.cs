////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: InfoController.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Controller to test the API and return some static information about the application.
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WhereToDo.Extensions;
using WhereToDo.Infrastructure;
using WhereToDo.Models;

namespace WhereToDo.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class InfoController : ControllerBase
    {
        private readonly ApplicationInfo _applicationInfo;

        public InfoController(IOptions<ApplicationInfo> applicationInfoAccessor)
        {
            _applicationInfo = applicationInfoAccessor.Value;
        }

        // [Authorize] // Not authorizing to show unauthorized requests vs. authorized requests with Token
        [HttpGet(Name = nameof(GetInfo))]
        [ResponseCache(CacheProfileName = "Static")] // 1 Day cache
        [Etag]
        public IActionResult GetInfo()
        {
            _applicationInfo.Href = Url.Link(nameof(GetInfo), null);

            if (!Request.GetEtagHandler().NoneMatch(_applicationInfo))
            {
                return StatusCode(304, _applicationInfo);
            }

            return Ok(_applicationInfo);
        }
    }
}
