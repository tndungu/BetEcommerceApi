using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Interfaces
{
    public interface ICartService
    {
        Task<int> AddToCart(CartRequest cart);
        Task<int> GetCartId();
        Task<int> GetCartItemsCount();
        Task<bool> UpdateCart(CartRequest cart);
        Task<bool> RemoveCartItem(int productId);
    }
}
