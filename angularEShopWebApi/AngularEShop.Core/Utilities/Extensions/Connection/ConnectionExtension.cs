using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularEShop.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace AngularEShop.Utilities.Extensions.Connection
{
    public static class ConnectionExtension
    {

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection service,IConfiguration configuration)
        {
            string connectionString = "ConnectionStrings:AngularEShopConnection:Development";
            service.AddEntityFrameworkSqlServer().AddDbContext<AngularEShopDbContext>((serviceProvider,Options) => 
            Options.UseSqlServer(configuration[connectionString]).UseInternalServiceProvider(serviceProvider));
            return service;
        }
    }
}
