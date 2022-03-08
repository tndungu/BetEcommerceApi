using BetEcommerce.Repository.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Order
{
    public class OrderItemsRepository : GenericRepository<OrderItem,int>, IOrderItemsRepository
    {
        public OrderItemsRepository(BetEcommerceDBContext context) : base(context)
        {

        }

    }
}
