using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Orders
{
    public class OrderDetail : BaseEntity
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product.Product Product { get; set; }
    }

}
