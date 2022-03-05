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
        private readonly ICartItemService _cartItemService;
        public CartService(BetEcommerceDBContext context,
            ICartItemService cartItemService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _cartItemService = cartItemService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddToCart(CartRequest cart)
        {
            var product = _context.Products.Where(x => x.Id == cart.ProductId).FirstOrDefault();
            if (product == null)
                throw new HttpException(HttpStatusCode.NotFound, "Product not found");
            
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Identity.Name);
            int cartId = 0;

            var cartExist = _context.Cart.Where(x => x.UserId == userId).FirstOrDefault();
            if(cartExist == null)
            {
                Cart cartAdd = new Cart();
                cartAdd.UserId = userId;
                _context.Cart.Add(cartAdd);
                await _context.SaveChangesAsync();
                cartId = cartAdd.Id;
            }else
                cartId = cartExist.Id;

            await _cartItemService.AddCartItem(cart, cartId, product.price);
            return _cartItemService.GetCartItemsCount(cartId);
        }
        public async Task<int> GetCartItemsCount()
        {
            var cartId = GetCartId().Result;
            int sum = 0;
            var cartItems = _context.CartItem.Where(x => x.CartId == cartId).ToList();
            if (cartItems.Any())
            {
                sum = cartItems.Sum(x => x.quantity);
            }
            return sum;
        }

        public async Task<int> GetCartId()
        {
            int cartId = 0;
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Identity.Name);
            var res = _context.Cart.Where(x => x.UserId == userId).FirstOrDefault();
            if(res != null)
                cartId = res.Id;
               
            return cartId;
        }

        public async Task<bool> UpdateCart(CartRequest cart)
        {
            int cartId = GetCartId().Result;
            if(cartId > 0)
            {
                var cartItem = _context.CartItem.Where(x => x.CartId == cartId && x.ProductId == cart.ProductId).FirstOrDefault();
                if (cartItem != null)
                {
                    var product = _context.Products.Where(x => x.Id == cart.ProductId).FirstOrDefault();
                    if(product != null)
                    {
                        cartItem.quantity = cart.Quantity;
                        cartItem.UnitPrice = product.price;
                        cartItem.TotalPrice = product.price * cart.Quantity;
                        _context.CartItem.Update(cartItem);
                    }
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveCartItem(int productId)
        {
            int cartId = GetCartId().Result;
            if (cartId > 0)
            {
                var cartItem = _context.CartItem.Where(x => x.CartId == cartId && x.ProductId == productId).FirstOrDefault();
                if (cartItem != null)
                {
                    _context.CartItem.Remove(cartItem);
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
