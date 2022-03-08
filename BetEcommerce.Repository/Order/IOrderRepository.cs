using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Order
{
    public interface IOrderRepository : IGenericRepository<Repository.EF.Order, int>
    {
        string GetOrderNumberByUserId(int userId);
    }
}
