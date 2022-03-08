using BetEcommerce.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Product
{
    public interface IProductRepository : IGenericRepository<Repository.EF.Product,int>
    {
        Task<List<Repository.EF.Product>> GetPagedProductsList(PointerParams @params);
    }
}
