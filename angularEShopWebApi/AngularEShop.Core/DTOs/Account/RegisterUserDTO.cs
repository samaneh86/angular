using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.DTOs.Account
{
    public class RegisterUserDTO
    {
        //[Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        //[Display(Name = "نام")]
        //[MaxLength(100, ErrorMessage = "ایمیل نباید بیشتر از 100 کاراکتر باشد")]
        public string Email { get; set; }


        //[Required(ErrorMessage = "لطفا نام را وارد کنید")]
        //[Display(Name = " نام خانوادگی")]
        //[MaxLength(100, ErrorMessage = "نام نباید بیشتر از 100 کاراکتر باشد")]
        public string FirstName { get; set; }



        //[Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        //[Display(Name = "نام")]
        //[MaxLength(100, ErrorMessage = "نام خانوادگی نباید بیشتر از 100 کاراکتر باشد")]
        public string LastName { get; set; }



        //[Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        //[Display(Name = "کلمه عبور ")]
        //[MaxLength(100, ErrorMessage = "کلمه عبور  نباید بیشتر از 100 کاراکتر باشد")]
        public string Password { get; set; }



        //[Required(ErrorMessage = "لطفا تکرار کلمه عبور را وارد کنید")]
        //[Display(Name = "تکرار کلمه عبور ")]
        //[MaxLength(100, ErrorMessage = "کلمه عبور  نباید بیشتر از 100 کاراکتر باشد")]
        //[Compare("Password",ErrorMessage ="کلمه های عبور مغایرت دارند")]
        public string ConfirmPassword { get; set; }




    //[Required(ErrorMessage = "لطفا آدرس را وارد کنید")]
    //    [Display(Name = "نام")]
    //    [MaxLength(500, ErrorMessage = "آدرس نباید بیشتر از 500 کاراکتر باشد")]
        public string Address { get; set; }


       public string EmailActivateCode { get; set; }
    }
    public enum RegisterUserResult
    {
       Success,
       EmailExists,
    }
}
