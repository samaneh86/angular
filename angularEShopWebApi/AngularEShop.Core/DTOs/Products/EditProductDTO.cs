using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.DTOs.Products
{
    public class EditProductDTO
    {
        public long Id { get; set; }

       // [Required(ErrorMessage = "لطفا نام محصول را وارد کنید")]
       // [Display(Name = "نام محصول")]
       // [MaxLength(150, ErrorMessage = "نام محصول بیشتر از 150 کاراکتر نباشد")]
        public string ProductName { get; set; }


      //  [Display(Name = "قیمت")]
      //  [MaxLength(100, ErrorMessage = "قیمت بیشتر از 150 کاراکتر نباشد")]
        public int Price { get; set; }

     //   [Required(ErrorMessage = "لطفا توضیحات کوتاه را وارد کنید")]
      //  [Display(Name = "توضیحات کوتاه")]
      //  [MaxLength(150, ErrorMessage = "توضیحات کوتاه بیشتر از 150 کاراکتر نباشد")]
        public string ShortDescription { get; set; }

     //   [Required(ErrorMessage = "لطفا توضیحات را وارد کنید")]
     //   [Display(Name = "توضیحات")]

        public string Description { get; set; }

      //  [Required(ErrorMessage = "لطفا نام تصویر را وارد کنید")]
      //  [Display(Name = "نام تصویر")]
     //   [MaxLength(100, ErrorMessage = "نام تصویر بیشتر از 100 کاراکتر نباشد")]
        public string CurrentImage { get; set; }

     //   [Display(Name = "موجود| یه اتمام رسیده")]
        public bool IsExists { get; set; }

      //  [Display(Name = "ویژه")]
        public bool IsSpecial { get; set; }
        public string Base64Image { get; set; }
    }
}
