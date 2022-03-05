using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Implementation
{
    public class CartItemService : ICartItemService
    {
        private readonly BetEcommerceDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartItemService(BetEcommerceDBContext context, 
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<CartResponse>> GetCartItems()
        {
            var user = _httpContextAccessor.HttpContext.User.Identity.Name;

            var cartResponse = (from c in _context.Cart
                                join ci in _context.CartItem on c.Id equals ci.CartId
                                join p in _context.Products on ci.ProductId equals p.Id
                                where c.UserId == Convert.ToInt32(user)

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
        public int GetCartItemsCount(int cartId)
        {
            int sum = 0;
            var cartItems = _context.CartItem.Where(x => x.CartId == cartId).ToList();
            if (cartItems.Any())
            {
                sum = cartItems.Sum(x => x.quantity);
            }
            return sum;
        }
        

        public async Task<bool> AddCartItem(CartRequest cart,int cartId,decimal productPrice)
        {
            _context.CartItem.Add(
                new CartItem
                {
                    CartId = cartId,
                    ProductId = cart.ProductId,
                    quantity = cart.Quantity,
                    UnitPrice = productPrice,
                    TotalPrice = productPrice * cart.Quantity
                });

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
