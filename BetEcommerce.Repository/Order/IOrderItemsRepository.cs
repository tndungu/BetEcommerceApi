using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Order
{
    public interface IOrderItemsRepository : IGenericRepository<Repository.EF.OrderItem,int>
    {

    }
}
