using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Interfaces
{
    public interface IOrderItemService
    {
        Task<bool> MoveCartItemsToOrderItems(int cartId, int orderId);
        Task<List<CartResponse>> GetOrderItems();
    }
}
