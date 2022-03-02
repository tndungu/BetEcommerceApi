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
        Task<bool> AddToCart(CartRequest cart);
        Task<List<CartResponse>> GetCartItems(int userId);
    }
}
