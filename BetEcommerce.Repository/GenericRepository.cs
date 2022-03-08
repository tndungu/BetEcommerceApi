using BetEcommerce.Repository.Repository.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository
{
    public class GenericRepository<T1, T2> : IGenericRepository<T1, T2> where T1 : class, new()
    {
        protected BetEcommerceDBContext context;

        public GenericRepository(BetEcommerceDBContext context) => this.context = context;

        public IQueryable<T1> GetAll()
        {
            return context.Set<T1>().AsQueryable();
        }

        public async Task<T1> GetByIdAsync(T2 id)
        {
            return await context.Set<T1>().FindAsync(id);
        }

        public async Task<T1> InsertAsync(T1 entity)
        {
            await context.Set<T1>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<T1> UpdateAsync(T1 entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            var result = await context.SaveChangesAsync();
            return result > 0 ? entity : null;
        }
        
        public async Task Delete(T2 id)
        {
            var entity = await context.Set<T1>().FindAsync(id);
            if(entity != null)
            {
                context.Remove(entity);
            }
            await context.SaveChangesAsync();
        }
        public  async Task<int> DeleteAsync(T1 entity)
        {
            context.Set<T1>().Remove(entity);
            return await context.SaveChangesAsync();
        }
    }
}
