using AngularEShop.Core.Services.Interfaces;
using AngularEShop.DataLayer.Entities.Access;
using AngularEShop.DataLayer.Entities.Account;
using AngularEShop.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AngularEShop.Core.Services.Implementations
{
  public  class AccessService:IAccessService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<Role> roleRepository;
        private readonly IGenericRepository<UserRole> userRoleRepository;
        public AccessService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository, IGenericRepository<UserRole> userRoleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;

        }
        public async Task<bool> CheckUserRole(long userId, string role)
        {
            return await userRoleRepository.GetEntitiesQuery().AsQueryable()
                .AnyAsync(s => s.UserId == userId && s.Role.Name == role);
        }
        public void Dispose()
        {
            userRepository?.Dispose();
            roleRepository?.Dispose();
            userRoleRepository?.Dispose();
        }
    }
}
