using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Interfaces
{
    public interface ICartItemService
    {
        Task<List<CartResponse>> GetCartItems();
        int GetCartItemsCount(int cartId);
        Task<bool> AddCartItem(CartRequest cart, int cartId, decimal productPrice);
    }
}
