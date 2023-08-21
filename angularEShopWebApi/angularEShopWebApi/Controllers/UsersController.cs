using AngularEShop.Core.Services.Interfaces;
using AngularEShop.DataLayer.Entities.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularEShopWebApi.Controllers
{

   
    public class UsersController : SiteBaseController
    {
        private IUserService userService;
        public UsersController(IUserService _userService)
        {
            userService = _userService;
        }
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return  new ObjectResult(userService.GetAllUsers()) ;

        }
    }
}
