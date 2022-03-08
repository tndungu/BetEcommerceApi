using BetEcommerce.Repository.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Repository.Cart
{
    public interface ICartItemRepository :IGenericRepository<CartItem,int>
    {
        List<CartItem> GetCartItemsListByCartId(int cartId);
        CartItem GetCartItemByProductId(int cartId, int productId);
    }
}
