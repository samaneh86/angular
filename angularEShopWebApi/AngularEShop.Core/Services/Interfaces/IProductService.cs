using AngularEShop.Core.DTOs.Products;
using AngularEShop.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.Services.Interfaces
{
   public interface IProductService:IDisposable
    {
       
        Task AddProduct (Product product);
        Task UpdateProduct(Product product);
        Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter);
       Task< List<ProductCategory>>  GetAllActiveProductCategories();
        Task<Product> GetProductById(long id);
        Task<List<ProductGallery>> GetProductActiveGalleries(long productId);
        Task<List<Product>> GetRelatedProducts(long productId);
        Task<ProductCommentDTO> AddproductComment(AddProductCommentDTO comment,long userId);
        Task<List<ProductCommentDTO>> GetActiveProductComments(long productId);
        Task<bool> IsExistsproductById(long productId);
        Task<Product> GetProductForUserOrder(long productId);
        Task<EditProductDTO> GetProductForEdit(long productId);
        Task EditProduct(EditProductDTO product);
    }
}
