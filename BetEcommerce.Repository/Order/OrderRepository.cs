using BetEcommerce.Repository.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Order
{
    public class OrderRepository : GenericRepository<Repository.EF.Order, int>, IOrderRepository
    {
        public OrderRepository(BetEcommerceDBContext context) : base(context)
        {
            
        }
        public string GetOrderNumberByUserId(int userId)
        {
            var orderNum = context.Orders.Where(x => x.UserId == userId).FirstOrDefault()?.OrderNumber;
            return orderNum;
        }
    }
}
