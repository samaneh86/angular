using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Site
{
    public class Slider:BaseEntity
    {
        [Required(ErrorMessage = "لطفا تصویر را وارد کنید")]
        [Display(Name = "نام تصویر")]
        [MaxLength(150, ErrorMessage = "نام تصویر بیشتر از 150 کاراکتر نباشد")]
        public string ImageName { get; set; }



        [Required(ErrorMessage="لطفا عنوان را وارد کنید")]
        [Display(Name="نام")]
        [MaxLength(100, ErrorMessage = "عنوان بیشتر از 100 کاراکتر نباشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "لطفا توضیحات را وارد کنید")]
        [Display(Name = "توضیحات")]
        [MaxLength(1000, ErrorMessage = "توضیحات بیشتر از 1000 کاراکتر نباشد")]
        public string Description { get; set; }

       
        [Display(Name = "لینک")]
        [MaxLength(100, ErrorMessage = "لینک بیشتر از 100 کاراکتر نباشد")]
        public string Link { get; set; }
    }
}
