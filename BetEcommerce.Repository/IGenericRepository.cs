using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository
{
    public interface IGenericRepository<T1,T2> where T1 : class
    {
        IQueryable<T1> GetAll();
        Task<T1> GetByIdAsync(T2 id);
        Task<T1> InsertAsync(T1 entity);
        Task Delete(T2 id);
        Task<int> DeleteAsync(T1 entity);
        Task<T1> UpdateAsync(T1 entity);
    }
}
