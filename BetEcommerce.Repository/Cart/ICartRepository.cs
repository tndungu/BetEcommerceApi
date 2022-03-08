using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Cart
{
    public interface ICartRepository : IGenericRepository<Repository.EF.Cart,int>
    {
        Repository.EF.Cart GetCartByUserId(int userId);
    }
}
