using AngularEShop.Core.Services.Interfaces;
using AngularEShop.Core.Utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AngularEShop.Core.DTOs.Products;
using angularEShopWebApi.identity;

namespace angularEShopWebApi.Controllers
{

    public class AdminProductsController : SiteBaseController
    {
        private readonly IProductService productService;
        public AdminProductsController(IProductService productService){
          this.productService=productService;
        }

        
        [HttpGet("get-product-for-edit/{id}")]
        [PermissionChecker("Admin")]
        public async Task<IActionResult> GetProductForEdit(long id)
        {
            var product = await productService.GetProductForEdit(id);
            if (product == null) 
                return JsonResponse.NotFound();
          else 
                return JsonResponse.Success(product);
        }

        [HttpPost("edit-product")]
        public async Task<IActionResult> EditProduct([FromBody] EditProductDTO product)
        {
         
            if (ModelState.IsValid)
            {
                await productService.EditProduct(product);
                return JsonResponse.Success();
            }
               
            else
                return JsonResponse.Error();

        }
    }
}
