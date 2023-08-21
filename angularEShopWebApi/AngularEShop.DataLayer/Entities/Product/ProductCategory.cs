using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Product
{
   public class ProductCategory:BaseEntity
    {
        [Required(ErrorMessage = "لطفا عنوان  را وارد کنید")]
        [Display(Name = "عنوان ")]
        [MaxLength(150, ErrorMessage = "عنوان  بیشتر از 150 کاراکتر نباشد")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا عنوان  را وارد کنید")]
        [Display(Name = "عتوان لینک ")]
        [MaxLength(150, ErrorMessage = "عنوان  لینک بیشتر از 150 کاراکتر نباشد")]
        public string UrlTitle { get; set; }
        public long? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public ProductCategory ParentCategory { get; set; }

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
    }
}
