using AngularEShop.DataLayer.Entities.Account;
using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Product
{
   public class ProductComment:BaseEntity
    {
        public long ProductId { get; set; }
        public long userId { get; set; }
        public string Text { get; set; }
        public virtual Product product { get; set; }
        public virtual  User User { get; set; }
    }
}
