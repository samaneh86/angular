using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularEShop.Core.Utilities.Common
{
    public static class JsonResponse
    {
        public static JsonResult Success()
        {
            return new JsonResult(new { status = "Success" });
        }
        public static JsonResult Success(Object returnData)
        {
            return new JsonResult(new { status = "Success" ,data=returnData});
        }
        public static JsonResult NotFound()
        {
            return new JsonResult(new { status = "NotFound" });
        }
        public static JsonResult NotFound(Object returnData)
        {
            return new JsonResult(new { status = "NotFound", data = returnData });
        }
        public static JsonResult Error()
        {
            return new JsonResult(new { status = "Error" });
        }
        public static JsonResult Error(Object returnData)
        {
            return new JsonResult(new { status = "Error", data = returnData });
        }

        public static JsonResult UnAuthorized()
        {
            return new JsonResult(new { status = "UnAuthorized" });
        }
        public static JsonResult UnAuthorized(Object returnData)
        {
            return new JsonResult(new { status = "UnAuthorized", data = returnData });
        }
        public static JsonResult NoAccess()
        {
            return new JsonResult(new { status = "NoAccess" });
        }
        public static JsonResult NoAccess(Object returnData)
        {
            return new JsonResult(new { status = "NoAccess", data = returnData });
        }
    }
}
