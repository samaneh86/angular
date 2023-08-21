using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.DTOs.Account
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "ایمیل نباید بیشتر از 100 کاراکتر باشد")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [Display(Name = "کلمه عبور ")]
        [MaxLength(100, ErrorMessage = "کلمه عبور  نباید بیشتر از 100 کاراکتر باشد")]
        public string Password { get; set; }

    }
    public enum LoginUserResult
    {
        Success,
        IncorrectData,
        NotActivated,
        NotAdmin
    }
}
