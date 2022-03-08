using BetEcommerce.Repository.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Cart
{
    public class CartRepository : GenericRepository<Repository.EF.Cart,int>, ICartRepository
    {
        public CartRepository(BetEcommerceDBContext context) : base(context)
        {

        }
        public Repository.EF.Cart GetCartByUserId(int userId)
        {
            var cart = context.Cart.Where(x => x.UserId == userId).FirstOrDefault();
            return cart;
        }
    }
}
