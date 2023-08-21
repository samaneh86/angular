using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Product
{
   public class ProductSelectedCategory:BaseEntity
    {
        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }

        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
