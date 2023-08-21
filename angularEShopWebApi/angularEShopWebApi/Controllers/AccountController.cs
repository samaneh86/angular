using AngularEShop.Core.DTOs.Account;
using AngularEShop.Core.Utilities.Identity;
using AngularEShop.Core.Services.Interfaces;
using AngularEShop.Core.Utilities.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AngularEShop.DataLayer.Entities.Account;
using AngularEShop.Core.Utilities.Convertor;
using AngularEShop.Core.Utilities.Identity;

namespace angularEShopWebApi.Controllers
{
  
    public class AccountController : SiteBaseController
    {
    
        private IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
           
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerData)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.RegisterUser(registerData);

                switch (res)
                {
                    case RegisterUserResult.EmailExists:
                        return JsonResponse.Error(new { Info = "EmailExists" });

                }
                return JsonResponse.Success();
            }

            return JsonResponse.Error();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.LoginUser(login);
                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponse.NotFound(new { message = "کاربری با مشخصات وارد شده یافت نشد" });

                    case LoginUserResult.NotActivated:
                        return JsonResponse.Error(new { message = "حساب کاربری شما فعال نشده است" });

                    case LoginUserResult.Success:
                        var user =await userService.GetUserByEmail(login.Email);
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEShopJwtBearer"));
                        var signincredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                            issuer: "https://localhost:44336",
                            claims: new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,user.Email),
                                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                            },
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: signincredentials
                            );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return JsonResponse.Success(new {
                            token = tokenString,
                            expireTime = 30, 
                            firstName = user.FirstName,
                            lastName = user.LastName,
                            id = user.Id,
                            address=user.Address 
                        });
                }

            }

            return JsonResponse.Error();
            }
        [HttpPost("check-user-auth")]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                long userId = User.GetUserId();
               User user = await userService.GetUserById(userId);

                return JsonResponse.Success(new
                {
                    email = user.Email,
                    address=user.Address,
                    userId=user.Id,
                    firstName=user.FirstName,
                    lastName=user.LastName

                }) ;
            }
            return JsonResponse.Error();
        }

        [HttpGet("SingOut")]
        public async Task<IActionResult> SingOut() { 
        
            if (User.Identity.IsAuthenticated)
            {
               await HttpContext.SignOutAsync();
                return JsonResponse.Success();
            }
            return JsonResponse.Error();
        }

        [HttpGet("activate-account/{id}")]
        public async Task<IActionResult> activateAccount(string id)
        {
            var user = await userService.GetUserByEmailActivateCode(id);
            if(user != null)
            {
                userService.ActivateUser(user);
                return JsonResponse.Success();
            }
            return JsonResponse.NotFound();

        }

        [HttpPost("edit-user")]
        public async Task<IActionResult> EditUser([FromBody]EditUserDTO editUser)
        {
            if (User.Identity.IsAuthenticated)
            {
                await userService.EditUserInfo(editUser, User.GetUserId());
                return JsonResponse.Success(new {message="اطلاعات کاربر با موفقیت ویرایش شد" });
            }
            return JsonResponse.UnAuthorized();

        }
    }
}
