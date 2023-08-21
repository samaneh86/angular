using AngularEShop.Core.DTOs.Account;
using AngularEShop.Core.Services.Interfaces;
using AngularEShop.DataLayer.Entities.Account;
using AngularEShop.Core.Utilities.Common;
using AngularEShop.Core.Utilities.Identity;
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

namespace angularEShopWebApi.Controllers
{
 
    public class AdminAccountController : SiteBaseController
    {
        private readonly IUserService userService;
            public AdminAccountController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.LoginUser(login,true);
                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponse.NotFound(new { message = "کاربری با مشخصات وارد شده یافت نشد" });

                    case LoginUserResult.NotActivated:
                        return JsonResponse.Error(new { message = "حساب کاربری شما فعال نشده است" });


                    case LoginUserResult.NotAdmin:
                        return JsonResponse.Error(new { message = " شما به این بخش دسترسی ندارید " });

                    case LoginUserResult.Success:
                        var user = await userService.GetUserByEmail(login.Email);
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
                        return JsonResponse.Success(new
                        {
                            token = tokenString,
                            expireTime = 30,
                            firstName = user.FirstName,
                            lastName = user.LastName,
                            id = user.Id,
                            address = user.Address
                        });
                }

            }

            return JsonResponse.Error();
        }

        [HttpPost("check-admin-auth")]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                
                var user = await userService.GetUserById(User.GetUserId());
                if(await userService.IsUserAdmin(user.Id))
                {
                    return JsonResponse.Success(new
                    {
                        email = user.Email,
                        address = user.Address,
                        userId = user.Id,
                        firstName = user.FirstName,
                        lastName = user.LastName

                    });
                }

                
            }
            return JsonResponse.Error();
        }
    }
}
