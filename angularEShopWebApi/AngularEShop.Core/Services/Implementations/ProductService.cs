using AngularEShop.Core.DTOs.Paging;
using AngularEShop.Core.DTOs.Products;
using AngularEShop.Core.Services.Interfaces;
using AngularEShop.DataLayer.Entities.Product;
using AngularEShop.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AngularEShop.Core.Utilities.Paging;
using static AngularEShop.Core.DTOs.Products.FilterProductsDTO;
using AngularEShop.Core.Utilities.Extensions.FileExtensions;
using AngularEShop.Core.Utilities.Common;

namespace AngularEShop.Core.Services.Implementations
{
    public class ProductService : IProductService
    {
        private IGenericRepository<Product> productRepository;
        private IGenericRepository<ProductGallery> productGalleryRepository;
        private IGenericRepository<ProductCategory> productCategoryRepository;
        private IGenericRepository<ProductVisit> productVisitRepository;
        private IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository;
        private IGenericRepository<ProductComment> productCommentRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductGallery> productGalleryRepository, IGenericRepository<ProductCategory> productCategoryRepository,
            IGenericRepository<ProductVisit> productVisitRepository, IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository, IGenericRepository<ProductComment> productCommentRepository)
        {
            this.productRepository = productRepository;
            this.productGalleryRepository = productGalleryRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.productVisitRepository = productVisitRepository;
            this.productSelectedCategoryRepository = productSelectedCategoryRepository;
            this.productCommentRepository = productCommentRepository;

        }
        public async Task AddProduct(Product product)
        {
            await productRepository.AddEntity(product);
            await productRepository.SaveChanges();
        }
        public async Task UpdateProduct(Product product)
        {
            productRepository.UpdateEntity(product);
            await productRepository.SaveChanges();
        }



        public async Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter)
        {
            var productsQuery = productRepository.GetEntitiesQuery().AsQueryable();
            switch (filter.OrderBy)
            {
                case ProductOrderBy.PriceAsc:
                    productsQuery = productsQuery.OrderBy(s => s.Price);
                    break;
                case ProductOrderBy.PriceDesc:
                    productsQuery = productsQuery.OrderByDescending(s => s.Price);
                    break;
            }


            if (!string.IsNullOrEmpty(filter.Title))
                productsQuery = productsQuery.Where(s => s.ProductName.Contains(filter.Title));


         
            if (filter.EndPrice != 0)
            {
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);
            }
            if (filter.StartPrice != 0)
            {
                productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice);
            }
            if (filter.Categories != null && filter.Categories.Any()) {
                productsQuery = productsQuery.SelectMany(s => s.ProductSelectedCategories.Where(f => filter.Categories.Contains(f.ProductCategoryId)).Select(t => t.Product));
            }
            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);
            var pager = Pager.Create(count, filter.PageId, filter.TakeEntity);
            var products = productsQuery.Paging(pager).ToList();
            return filter.SetProducts(products).SetPaging(pager);

        }
        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await productCategoryRepository.GetEntitiesQuery().Where(s => !s.IsDelete).ToListAsync();
        }

        public async Task<Product> GetProductById(long id)
        {
            return await productRepository.GetEntitiesQuery().AsQueryable()
                .SingleOrDefaultAsync(s=>!s.IsDelete && s.Id==id);
        }

        public async Task<List<ProductGallery>> GetProductActiveGalleries(long productId)
        {
            return await productGalleryRepository.GetEntitiesQuery().Where(s => s.ProductId == productId && !s.IsDelete).Select(s => new ProductGallery {
                ProductId = s.ProductId,
                Id = s.Id,
                ImageName = s.ImageName,
                CreatedDate = s.CreatedDate


            }).ToListAsync();
        }

        public async Task<List<Product>> GetRelatedProducts(long productId)
        {
            var product = productRepository.GetEntityById(productId);
            if (product == null) return null;
            var productCategoriesList = productSelectedCategoryRepository
                .GetEntitiesQuery().Where(s => s.ProductId == productId)
                .Select(f => f.ProductCategoryId).ToList();

            var relatedProducts = productRepository.GetEntitiesQuery()
                .SelectMany(
                s => s.ProductSelectedCategories
                .Where(f => productCategoriesList.Contains(f.ProductCategoryId))
                .Select(t => t.Product)
                )
                .Where(s => s.Id != productId)
                .OrderByDescending(s => s.CreatedDate).Take(4).ToListAsync();
            return await relatedProducts;
        }

       

        public async Task<List<ProductCommentDTO>> GetActiveProductComments(long productId)
        {

            return await productCommentRepository.GetEntitiesQuery()
                .Include(c => c.User)
                .Where(c => c.ProductId == productId && !c.IsDelete)
                .OrderByDescending(c => c.CreatedDate)
                .Select(s => new ProductCommentDTO {
                    Id = s.Id,
                    Text = s.Text,
                    UserId = s.userId,
                    UserFullName = s.User.FirstName + " " + s.User.LastName,
                    CreateDate = s.CreatedDate.ToString("yyyy/NMM/dd HH:mm")
                }).ToListAsync();
        }

        public async Task<ProductCommentDTO> AddproductComment(AddProductCommentDTO comment, long userId)
        {

            var commentData = new ProductComment
            {
                ProductId = comment.ProductId,
                Text = comment.Text,
                userId = userId
            };
            await productCommentRepository.AddEntity(commentData);
            await productCommentRepository.SaveChanges();
            return new ProductCommentDTO
            {
                Id = commentData.Id,
                UserId = userId,
                Text = commentData.Text,
                UserFullName = "",
                CreateDate = commentData.CreatedDate.ToString("yyyy/NMM/dd HH:mm"),
               
                
              


            };
        }
       public async Task<bool> IsExistsproductById(long productId)
        {
            return await productRepository.GetEntitiesQuery().AnyAsync(s => s.Id == productId);
        }
        public async Task<Product> GetProductForUserOrder(long productId)
        {
            return await productRepository.GetEntitiesQuery().FirstOrDefaultAsync(s => s.Id == productId && !s.IsDelete);
        }

        public async Task<EditProductDTO> GetProductForEdit(long productId)
        {
            var product = await productRepository.GetEntitiesQuery().AsQueryable()
              .SingleOrDefaultAsync(s => s.Id == productId);
            if (product == null) return null;
            return new EditProductDTO
            {
                Id = product.Id,
                CurrentImage = product.ImageName,
                Description = product.Description,
                IsExists = product.IsExists,
                IsSpecial = product.IsSpecial,
                Price = product.Price,
                ProductName = product.ProductName,
                ShortDescription = product.ShortDescription

            };
        }
        public async Task EditProduct(EditProductDTO product)
        {
            var mainProduct = await productRepository.GetEntityById(product.Id);
            if (mainProduct != null)
            {
                mainProduct.ProductName = product.ProductName;
                mainProduct.Description = product.ProductName;
                mainProduct.IsExists = product.IsExists;
                mainProduct.IsSpecial = product.IsSpecial;
                mainProduct.Price = product.Price;
                mainProduct.Description = product.Description;
                mainProduct.ShortDescription = product.ShortDescription;
                if (!string.IsNullOrEmpty(product.Base64Image))
                {
                    var imageFile = ImageUploaderExtension.Base64ToImage(product.Base64Image);
                    var imageName = Guid.NewGuid().ToString("N") + ".jpeg";
                    imageFile.AddImageToServer(imageName, PathTools.productImageServerPath, mainProduct.ImageName);
                    mainProduct.ImageName = imageName;
                }
                productRepository.UpdateEntity(mainProduct);
                await productRepository.SaveChanges();
            }

        }
      
        public void Dispose()
        {
            productRepository?.Dispose();
            productCategoryRepository?.Dispose();
            productGalleryRepository?.Dispose();
            productVisitRepository?.Dispose();
            productSelectedCategoryRepository?.Dispose();
            productCommentRepository?.Dispose();
        }
    }
}
