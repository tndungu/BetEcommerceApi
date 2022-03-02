using BetEcommerce.Model.Request;
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

            //var cartUser = _context.Cart.Where(x => x.UserId == cart.UserId).FirstOrDefault();

            var cartRecord = JsonConvert.DeserializeObject<Cart>(JsonConvert.SerializeObject(cart));
            
            _context.Cart.Add(cartRecord);
            await _context.SaveChangesAsync();

            var cartItem = new CartItem
            {
                CartId = cartRecord.Id,
                ProductId = cart.ProductId,
                quantity = cart.Quantity,
                UnitPrice = product.price,
                TotalPrice = product.price * cart.Quantity
            };
            _context.CartItem.Add(cartItem);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
