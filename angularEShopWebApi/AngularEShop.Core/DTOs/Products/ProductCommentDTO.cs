using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.DTOs.Products
{
    public class ProductCommentDTO
    {
        public string Text { get; set; }
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public string CreateDate { get; set; }
    }
    public enum AddProductCommentResult
    {
        Success,
        NotFoundProduct,
        Error
    }
}
