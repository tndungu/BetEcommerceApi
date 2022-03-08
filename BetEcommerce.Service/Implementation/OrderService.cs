using BetEcommerce.Repository.Cart;
using BetEcommerce.Repository.Order;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Repository.User;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace BetEcommerce.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderItemService _orderItemService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IUserRepository userRepository,
            IOrderItemService orderItemService,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _orderItemService = orderItemService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Order()
        {
            int userId = GetUserId();

            var cart = _cartRepository.GetCartByUserId(userId);

            var orderRecord = new Order
            {
                UserId = userId,
                OrderNumber = $"1100{cart.Id}/2022",
            };

            await _orderRepository.InsertAsync(orderRecord);
            var message = BuildMessage();

            await _orderItemService.MoveCartItemsToOrderItems(cart.Id, orderRecord.Id);
            await _cartRepository.DeleteAsync(cart);
            var user = await _userRepository.GetByIdAsync(userId);

            _emailService.SendEmailAsync(message, user.Email);
            return true;
        }
        public string GetOrderNumber()
        {
            int userId = GetUserId();
            return _orderRepository.GetOrderNumberByUserId(userId);
        }
        public int GetUserId()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.User.Identity.Name);
        }

        #region Private Methods
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
        #endregion
    }
}
