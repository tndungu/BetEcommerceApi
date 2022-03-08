using BetEcommerce.Repository.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Cart
{
    public class CartItemRepository : GenericRepository<CartItem,int>,ICartItemRepository
    {
        public CartItemRepository(BetEcommerceDBContext context) : base(context)
        {
        }

        public List<CartItem> GetCartItemsListByCartId(int cartId)
        {
            var itemsList = context.CartItem.Where(x => x.CartId == cartId)?.ToList();
            return itemsList;
        }
        public CartItem GetCartItemByProductId(int cartId, int productId)
        {
            var item = context.CartItem.Where(x=> x.ProductId == productId && x.CartId == cartId).FirstOrDefault();
            return item;
        }
    }
}
