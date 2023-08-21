using AngularEShop.Core.Services.Interfaces;
using AngularEShop.DataLayer.Entities.Access;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularEShop.Core.Utilities.Identity;
using AngularEShop.Core.Utilities.Common;

namespace angularEShopWebApi.identity
{
    public class PermissionCheckerAttribute : AuthorizeAttribute,IAuthorizationFilter
    {

        private string _role;
        private IAccessService _accessService;
        public PermissionCheckerAttribute(string role)
        {
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _accessService =(IAccessService) context.HttpContext.RequestServices.GetService(typeof(IAccessService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetUserId();
                var result = _accessService.CheckUserRole(userId, _role).Result;
                if (!result)
                {
                    context.Result = JsonResponse.NoAccess();
                }
            }
            else
            {
                context.Result = JsonResponse.NoAccess();
            }
        }

    }
}
