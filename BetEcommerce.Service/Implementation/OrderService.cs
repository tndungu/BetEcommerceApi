using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace BetEcommerce.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly BetEcommerceDBContext _context;
        private readonly IOrderItemService _orderItemService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(
            BetEcommerceDBContext context,
            IOrderItemService orderItemService,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _orderItemService = orderItemService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Order()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Identity.Name);

            var cart = _context.Cart.Where(x => x.UserId == userId).FirstOrDefault();
            
            var orderRecord = new Order
            {
                UserId = userId,
                OrderNumber = $"1100{cart.Id}/2022",
            };
            _context.Orders.Add(orderRecord);
            await _context.SaveChangesAsync();

            _orderItemService.MoveCartItemsToOrderItems( cart.Id,orderRecord.Id);
            _context.Cart.Remove(cart);

            await _context.SaveChangesAsync();

            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var message = BuildMessage();

            _emailService.SendEmailAsync(message, user.Email);
            return true;
        }

        private string BuildMessage()
        {
            var orderItems = _orderItemService.GetOrderItems().Result;
            var totalAmount = 0m;

            var message = new StringBuilder($"<h2>ORDER NUMBER: BET_{orderItems[0].OrderId}</h2>");
            message.Append("<table><tr><th>Product Name</th><th>Quantity</th><th>Price</th></tr>");
            orderItems.ForEach(item =>
            {
                totalAmount += item.TotalPrice;

                message.Append($"<tr><td>{item.ProductName}</td><td>{item.Quantity}</td><td>{item.TotalPrice}</td></tr>");
            });
            message.Append($"<tr><td>TOTAL AMOUNT</td><td></td><td><b>{totalAmount}</b></td></tr></table>");
            return message.ToString();
        }
    }
}
