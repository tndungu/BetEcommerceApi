using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Helpers;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly BetEcommerceDBContext _context;
        private readonly IEmailService _emailService;
        public OrderService(BetEcommerceDBContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<bool> Order(int userId)
        {
            var carts = _context.Cart.Where(x => x.UserId == userId).ToList();
            carts.ForEach(async cart =>
            {
                var cartItems = _context.CartItem.Where(x => x.CartId == cart.Id).ToList();
                int orderId = await AddCartToOrder(cart);
                
                await AddCartItemsToOrderItems(cartItems,orderId);
                RemoveFromCart(cart);
            });

            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var message = BuildMessage(userId);

            _emailService.SendEmailAsync(message, user.Email);
            return true;
        }

        public async Task<List<CartResponse>> GetOrderItems(int userId)
        {
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
        private string BuildMessage(int userId)
        {
            var orderItems = GetOrderItems(userId).Result;
            var totalAmount = 0m;

            var message = new StringBuilder($"<h2>ORDER NUMBER: BET_{orderItems[0].OrderId}</h2>");
            message.Append("<table><tr><th>Product Name</th><th>Quantity</th><th>Price</th></tr>");
            orderItems.ForEach(item => {
                totalAmount += item.TotalPrice;

                message.Append($"<tr><td>{item.ProductName}</td><td>{item.Quantity}</td><td>{item.TotalPrice}</td></tr>");
            });
            message.Append($"<tr><td>TOTAL AMOUNT</td><td></td><td><b>{totalAmount}</b></td></tr></table>");
            return message.ToString();
        }

        private async Task<int> AddCartToOrder(Cart cart)
        {
            var order = JsonConvert.DeserializeObject<Order>(JsonConvert.SerializeObject(cart));
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        private async void RemoveFromCart(Cart cart)
        {
            _context.Cart.Remove(cart);
            _context.SaveChanges();
        }

        private async Task<bool> AddCartItemsToOrderItems(List<CartItem> items, int orderId)
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
