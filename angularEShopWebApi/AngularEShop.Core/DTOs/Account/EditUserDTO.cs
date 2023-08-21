using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.DTOs.Account
{
    public class EditUserDTO
    {
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "نام نباید بیشتر از 100 کاراکتر باشد")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "نام خانوادگی نباید بیشتر از 100 کاراکتر باشد")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [Display(Name = "آدرس")]
        [MaxLength(500, ErrorMessage = "نام نباید بیشتر از 100 کاراکتر باشد")]
        public string Address { get; set; }
    }
}
