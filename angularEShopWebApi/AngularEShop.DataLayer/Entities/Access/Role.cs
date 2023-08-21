using AngularEShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Entities.Access
{
   public class Role:BaseEntity
    {
        [Required(ErrorMessage = "لطفا نام سیستمی را وارد کنید")]
        [Display(Name = " نام سیستمی ")]
        [MaxLength(100, ErrorMessage = "نام سیستمی نباید بیشتر از 100 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = "عنوان نباید بیشتر از 100 کاراکتر باشد")]
        public string Title { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
