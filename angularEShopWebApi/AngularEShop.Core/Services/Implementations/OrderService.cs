using AngularEShop.Core.Services.Interfaces;
using AngularEShop.DataLayer.Entities.Orders;
using AngularEShop.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AngularEShop.Core.DTOs.Orders;
using AngularEShop.Core.Utilities.Common;

namespace AngularEShop.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository,
            IUserService userService, IProductService productService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userService = userService;
            _productService = productService;
        }
        public async Task<Order> CreateUserOrder(long userId)
        {
            var order = new Order
            {
                UserId = userId
            };
            await _orderRepository.AddEntity(order);
            await _orderRepository.SaveChanges();
            return order;
        }

        public async Task<Order> GetUserOpenOrder(long userId)
        {
            var order = _orderRepository.GetEntitiesQuery()
                .Include(s => s.OrderDetails)
                .ThenInclude(s => s.Product)
                .SingleOrDefault(s => s.UserId == userId && !s.IsPay && !s.IsDelete);
            if (order == null)
                order = await CreateUserOrder(userId);
            return order;
        }
        public async Task AddProductToOrder(long userId, long productId, int count)
        {
            var user = await _userService.GetUserById(userId);
            var product = await _productService.GetProductForUserOrder(productId);
            if (user != null && product != null)
            {
                var order = await GetUserOpenOrder(userId);
                if (count < 1) count = 1;

                var details = await GetOrderDetails(order.Id);
                var existsDetail = details.SingleOrDefault(s => s.ProductId == productId && !s.IsDelete);
                if (existsDetail != null)
                {
                    existsDetail.Count += count;
                    _orderDetailRepository.UpdateEntity(existsDetail);

                }

                else
                {
                    var detail = new OrderDetail
                    {
                     
                        OrderId = order.Id,
                        ProductId = productId,
                        Price = product.Price,
                        Count = count
                    };
                    await _orderDetailRepository.AddEntity(detail);
                }

                await _orderDetailRepository.SaveChanges();
            }
        }

        public async Task<List<OrderDetail>> GetOrderDetails(long orderId)
        {
            return await _orderDetailRepository.GetEntitiesQuery().Where(s => s.OrderId == orderId).ToListAsync();
        }

        public async Task<List<OrderBasketDetail>> GetUserBasketDetails(long userId)
        {
            var openOrder = await GetUserOpenOrder(userId);
            if (openOrder == null) return null;
            return openOrder.OrderDetails.Where(s=>!s.IsDelete).Select(f => new OrderBasketDetail {
                Id = f.Id,
                Count = f.Count,
                price = f.Price,
                Title = f.Product.ProductName,
                ImageName = PathTools.Domain + PathTools.productImagePath + f.Product.ImageName


            }).ToList();
        }
       public async Task DeleteOrderDetail(OrderDetail detail)
        {
            _orderDetailRepository.RemoveEntity(detail);
            await _orderDetailRepository.SaveChanges();
        }




        public void Dispose()
        {
            _orderRepository?.Dispose();
            _orderDetailRepository?.Dispose();
        }


    }
}
