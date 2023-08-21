using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Product
{
   public class ProductVisit:BaseEntity
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage = "لطفا Ip را وارد کنید")]
        [Display(Name = "IP")]
        [MaxLength(150, ErrorMessage = "نام تصویر بیشتر از 150 کاراکتر نباشد")]
        public string UserIp { get; set; }
        public Product Product { get; set; }
    }
}
