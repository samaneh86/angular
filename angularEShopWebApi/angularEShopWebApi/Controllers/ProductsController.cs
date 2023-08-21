using AngularEShop.Core.DTOs.Products;
using AngularEShop.Core.Services.Interfaces;
using AngularEShop.Core.Utilities.Identity;
using AngularEShop.DataLayer.Entities.Product;
using AngularEShop.Core.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularEShopWebApi.Controllers
{
    public class ProductsController : SiteBaseController
    {
        private IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDTO filter)
        {
            var products = await productService.FilterProducts(filter);
            return JsonResponse.Success(products);
        }
        [HttpGet("GetProductsCategories")]
        public async Task<IActionResult> GetProductsCategories()
        {
            return JsonResponse.Success(await productService.GetAllActiveProductCategories());
        }
        [HttpGet("single-product/{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await productService.GetProductById(id);
            var productGalleries = productService.GetProductActiveGalleries(id);
            if (product != null)
                return JsonResponse.Success(new { product = product, galleries = productGalleries.Result });
            else
                return JsonResponse.NotFound();
        }
        [HttpGet("related-products/{id}")]
        public async Task<IActionResult> GetRelatedProducts(long id)
        {
            var relatedProducts =await productService.GetRelatedProducts(id);
            return JsonResponse.Success(relatedProducts);
        }

        [HttpGet("product-comment/{id}")]
        public async Task<IActionResult> GetProductComments(long id)
        {
            var comments = await productService.GetActiveProductComments(id);
            return JsonResponse.Success(comments);
        }

        [HttpPost("add-product-comment")]
        public async Task<IActionResult> AddProductComments([FromBody]AddProductCommentDTO comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return JsonResponse.Error(new { message = "لطفا ابتدا وارد سایت شوید" });

            }
            if(!await  productService.IsExistsproductById(comment.ProductId))
            {
                return JsonResponse.NotFound();
            }
            var userId = User.GetUserId();
          var res= await productService.AddproductComment(comment, userId);
            return JsonResponse.Success(res);
        }

    }


       
    
}
