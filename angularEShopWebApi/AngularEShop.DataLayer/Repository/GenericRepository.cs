using AngularEShop.DataLayer.Context;
using AngularEShop.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.DataLayer.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private AngularEShopDbContext context;
        private DbSet<TEntity> dbSet;
        public GenericRepository(AngularEShopDbContext _context)
        {
            context = _context;
            dbSet = context.Set<TEntity>();
        }
       
   

        public IQueryable<TEntity> GetEntitiesQuery()
        {
            return dbSet.AsQueryable();
        }

        public async Task<TEntity> GetEntityById(long entityId)
        {
            return await dbSet.SingleOrDefaultAsync(e => e.Id == entityId);
        }
        public async Task AddEntity(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.LastUpdateDate = entity.CreatedDate;
            await dbSet.AddAsync(entity);
        }
        public void UpdateEntity(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            dbSet.Update(entity);
        }
        public void RemoveEntity(TEntity entity)
        {
            entity.IsDelete = true;
            UpdateEntity(entity);
        }

        public async Task RemoveEntity(long entityId)
        {
           var entity= await GetEntityById(entityId);
            RemoveEntity(entity);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

     
        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
