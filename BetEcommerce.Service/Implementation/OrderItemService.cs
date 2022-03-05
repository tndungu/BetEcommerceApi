using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Implementation
{
    public class OrderItemService : IOrderItemService
    {
        private readonly BetEcommerceDBContext _context;
        
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderItemService(
            BetEcommerceDBContext context,
            
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<CartResponse>> GetOrderItems()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Identity.Name);
            var cartResponse = (from c in _context.Orders
                                join ci in _context.OrdersItem on c.Id equals ci.OrderId
                                join p in _context.Products on ci.ProductId equals p.Id
                                where c.UserId == userId

                                select new CartResponse
                                {
                                    ImageId = p.ImageId,
                                    ProductId = p.Id,
                                    ProductName = p.Name,
                                    Quantity = ci.quantity,
                                    TotalPrice = ci.TotalPrice
                                }).ToList();
            return cartResponse;
        }
        public async Task<bool> MoveCartItemsToOrderItems(int cartId, int orderId)
        {
            var cartItemList = _context.CartItem.Where(x => x.CartId == cartId).ToList();
            var orderListItems = DeserializeCartItems(cartItemList, orderId);
            _context.OrdersItem.AddRange(orderListItems);
            _context.CartItem.RemoveRange(cartItemList);
            return await _context.SaveChangesAsync() > 0;
        }

        private List<OrderItem> DeserializeCartItems(List<CartItem> list, int orderId)
        {
            var item = JsonConvert.SerializeObject(list);
            var orderListItems = JsonConvert.DeserializeObject<List<OrderItem>>(item);

            foreach (var orderItem in orderListItems)
            {
                orderItem.OrderId = orderId;
                orderItem.Id = 0;
            }
            return orderListItems;
        }
        private async Task<bool> MoveCartItemsToOrderItems(List<CartItem> items, int orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var item in items)
            {
                var orderItem = JsonConvert.DeserializeObject<OrderItem>(JsonConvert.SerializeObject(item));
                orderItem.OrderId = orderId;
                orderItems.Add(orderItem);
            }

            await _context.OrdersItem.AddRangeAsync(orderItems);
            _context.CartItem.RemoveRange(items);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
