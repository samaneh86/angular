using AngularEShop.Core.Services.Interfaces;
using AngularEShop.Core.Utilities.Identity;
using AngularEShop.Core.Utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularEShopWebApi.Controllers
{

    public class OrderController : SiteBaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("add-order")]
        public async Task<IActionResult> AddProductToOrder(long productId, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();

                await _orderService.AddProductToOrder(userId, productId, count);
                return JsonResponse.Success(new
                { message = "محصول با موفقیت به سبد خرید شما افزوده شد",
                    details = await _orderService.GetUserBasketDetails(userId)
                });
            }
            return JsonResponse.Error(new { message = "برای افزودن محصول به سبد خید ابتدا لاگین کنید"

            });
        }

        [HttpGet("get-order-detail")]
        public async Task<IActionResult> GetUserBasketDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                var details = await _orderService.GetUserBasketDetails(User.GetUserId());
                return JsonResponse.Success(details);
            }
            return JsonResponse.Error();
        }

        [HttpGet("remove-order-detail/{detailId}")]
        public async Task<IActionResult> RemoveOrderdetail(int detailId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userOpenOrder = await _orderService.GetUserOpenOrder(User.GetUserId());
                var detail = userOpenOrder.OrderDetails.SingleOrDefault(s => s.Id == detailId);
                if(detail != null)
                {
                    await _orderService.DeleteOrderDetail(detail);
                    return JsonResponse.Success(await _orderService.GetUserBasketDetails(User.GetUserId()));
                }
            }
            return JsonResponse.Error();
        }
    }
}
