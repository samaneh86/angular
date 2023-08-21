using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.DTOs.Products
{
  public  class AddProductCommentDTO
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage="لطفا نظر خود را وارد کنید")]
        [MaxLength(ErrorMessage = "متن وارد شده نباید بیشتر از 1000 کاراکتر باشد")]
        public string Text { get; set; }
    }
}
