////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: RootController.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Starting point of the API which returns all routes available.
 * 
 * Based off referenced Lynda.com course
 * 
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToDo.Models;

namespace WhereToDo.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Info = Link.To(nameof(InfoController.GetInfo)),
                Users = Link.To(nameof(UsersController.GetVisibleUsers)),
                UserInfo = Link.To(nameof(UserinfoController.Userinfo)),
                List = Link.To(nameof(ListController.GetListsAsync)),
                Token = Link.To(nameof(TokenController.TokenExchange))
            };

            return Ok(response);
        }
    }
}
