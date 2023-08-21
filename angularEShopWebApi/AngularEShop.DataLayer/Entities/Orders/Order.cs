using AngularEShop.DataLayer.Entities.Account;
using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Orders
{
    public class Order : BaseEntity
    {
        public long UserId { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PaymentDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }

}
