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
            int userId = GetUserId();

            var cart = _context.Cart.Where(x => x.UserId == userId).FirstOrDefault();

            var orderRecord = new Order
            {
                UserId = userId,
                OrderNumber = $"1100{cart.Id}/2022",
            };
            _context.Orders.Add(orderRecord);
            await _context.SaveChangesAsync();
            var message = BuildMessage();
            await _orderItemService.MoveCartItemsToOrderItems(cart.Id, orderRecord.Id);
            _context.Cart.Remove(cart);

            await _context.SaveChangesAsync();

            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();

            _emailService.SendEmailAsync(message, user.Email);
            return true;
        }

        private string BuildMessage()
        {
            var orderItems = _orderItemService.GetOrderItems().Result;
            var orderNumber = GetOrderNumber();
            var totalAmount = 0m;

            var message = new StringBuilder("<h2>Your BET Order Summary</h2>");
            message.Append($"<h3>ORDER NUMBER: {orderNumber}</h3>");
            
            message.Append("<table><tr><th>Product Name</th><th>Quantity</th><th>Price</th></tr>");
            orderItems.ForEach(item =>
            {
                totalAmount += item.TotalPrice;
                message.Append($"<tr><td>{item.ProductName}</td><td>{item.Quantity}</td><td>{item.TotalPrice}</td></tr>");
            });
            message.Append($"<tr><td>TOTAL AMOUNT</td><td></td><td><b>{totalAmount}</b></td></tr></table>");
            return message.ToString();
        }
        private string GetOrderNumber()
        {
            int userId = GetUserId();
            return _context.Orders.Where(x => x.UserId == userId).FirstOrDefault()?.OrderNumber;
        }
        public int GetUserId()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.User.Identity.Name);
        }
    }
}
