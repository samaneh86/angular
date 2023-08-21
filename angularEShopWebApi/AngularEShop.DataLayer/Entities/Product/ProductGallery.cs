using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Product
{
    public class ProductGallery:BaseEntity
    {
     
        public long ProductId { get; set; }

        [Required(ErrorMessage = "لطفا نام تصویر را وارد کنید")]
        [Display(Name = "عکس")]
        [MaxLength(150, ErrorMessage = "نام تصویر بیشتر از 150 کاراکتر نباشد")]
        public string ImageName { get; set; }
        public Product Product { get; set; }
    }
}
