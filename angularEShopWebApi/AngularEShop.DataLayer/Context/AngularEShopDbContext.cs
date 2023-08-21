using AngularEShop.DataLayer.Entities.Access;
using AngularEShop.DataLayer.Entities.Account;
using AngularEShop.DataLayer.Entities.Orders;
//using AngularEShop.DataLayer.Entities.Orders;
using AngularEShop.DataLayer.Entities.Product;
using AngularEShop.DataLayer.Entities.Site;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Context
{
   public class AngularEShopDbContext : DbContext
    {
        public AngularEShopDbContext(DbContextOptions<AngularEShopDbContext> options):base(options){
            }
     
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGallery> ProductGalleries { get; set; }
        public virtual DbSet<ProductVisit> ProductVisits { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductSelectedCategory> ProductselectedCategories { get; set; }
       public virtual DbSet<ProductComment> ProductComments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
       public virtual DbSet<OrderDetail> OrderDetails { get; set; }


    }
}
