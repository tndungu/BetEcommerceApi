using BetEcommerce.Model.Request;
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
    public class CartService : ICartService
    {
        private readonly BetEcommerceDBContext _context;
        public CartService(BetEcommerceDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToCart(CartRequest cart)
        {
            var product = _context.Products.Where(x => x.Id == cart.ProductId).FirstOrDefault();
            if (product == null)
                throw new HttpException(HttpStatusCode.NotFound, "Product not found");

            Cart cartAdd = new Cart();
            int cartId = 0;
            cartAdd = _context.Cart.Where(x => x.UserId == cart.UserId).FirstOrDefault();
            if(cartAdd == null)
            {
                cartAdd.UserId = cart.UserId;
                _context.Cart.Add(cartAdd);

                await _context.SaveChangesAsync();
                cartId = cartAdd.Id;
            }else
                cartId = cartAdd.Id;
          
            _context.CartItem.Add(
                new CartItem
            {
                CartId = cartId,
                ProductId = cart.ProductId,
                quantity = cart.Quantity,
                UnitPrice = product.price,
                TotalPrice = product.price * cart.Quantity
            });

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<CartResponse>> GetCartItems(int userId)
        {
            var cartResponse = (from c in _context.Cart
                                join ci in _context.CartItem on c.Id equals ci.CartId
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
    }
}
