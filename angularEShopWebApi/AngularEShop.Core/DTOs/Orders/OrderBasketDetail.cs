using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.DTOs.Orders
{
    public class OrderBasketDetail
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int price { get; set; }
        public string ImageName { get; set; }
        public int Count { get; set; }
    }
}
