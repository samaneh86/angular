using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEShop.DataLayer.Entities.Access;
using AngularEShop.DataLayer.Entities.Common;
using AngularEShop.DataLayer.Entities.Orders;
//using AngularEShop.DataLayer.Entities.Orders;
using AngularEShop.DataLayer.Entities.Product;

namespace AngularEShop.DataLayer.Entities.Account
{
    public class User:BaseEntity
    {
        [Required(ErrorMessage="لطفا ایمیل را وارد کنید")]
        [Display(Name="نام")]
        [MaxLength(100,ErrorMessage ="ایمیل نباید بیشتر از 100 کاراکتر باشد")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "نام نباید بیشتر از 100 کاراکتر باشد")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "نام خانوادگی نباید بیشتر از 100 کاراکتر باشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [Display(Name = "کلمه عبور ")]
        [MaxLength(100, ErrorMessage = "کلمه عبور  نباید بیشتر از 100 کاراکتر باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [Display(Name = "نام")]
        [MaxLength(500, ErrorMessage = "نام نباید بیشتر از 100 کاراکتر باشد")]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = "نام نباید بیشتر از 100 کاراکتر باشد")]
        public string EmailActivateCode { get; set; }
        public bool IsActivated { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<ProductComment> ProductComemnts { get; set; }
        public virtual ICollection<Order> Order { get; set; }

    }
}
