using AngularEShop.Core.DTOs.Orders;
using AngularEShop.DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.Services.Interfaces
{
  public  interface IOrderService:IDisposable
    {
     Task<Order>CreateUserOrder(long userId);
       Task<Order>GetUserOpenOrder(long UserId);
       Task AddProductToOrder(long userId, long productId, int count);
        Task<List<OrderDetail>> GetOrderDetails(long orderId);
        Task<List<OrderBasketDetail>> GetUserBasketDetails(long userId);
        Task DeleteOrderDetail(OrderDetail detail);
    }
}
