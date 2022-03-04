using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Repository.Helpers;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly BetEcommerceDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(BetEcommerceDBContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddToCart(CartRequest cart)
        {
            var product = _context.Products.Where(x => x.Id == cart.ProductId).FirstOrDefault();
            if (product == null)
                throw new HttpException(HttpStatusCode.NotFound, "Product not found");
            
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Identity.Name);
            Cart cartAdd = new Cart();
            int cartId = 0;

            var cartExist = _context.Cart.Where(x => x.UserId == userId).FirstOrDefault();
            if(cartExist == null)
            {
                cartAdd.UserId = userId;
                _context.Cart.Add(cartAdd);
                await _context.SaveChangesAsync();
                cartId = cartAdd.Id;
            }else
                cartId = cartExist.Id;

            _context.CartItem.Add(
                new CartItem
            {
                CartId = cartId,
                ProductId = cart.ProductId,
                quantity = cart.Quantity,
                UnitPrice = product.price,
                TotalPrice = product.price * cart.Quantity
            });

            await _context.SaveChangesAsync();
            int cartItemsCount = GetCartItemsCount(cartId);
            return cartItemsCount;
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
    }
}
