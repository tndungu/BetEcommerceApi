using BetEcommerce.Model.Request;
using BetEcommerce.Repository.Cart;
using BetEcommerce.Repository.Helpers;
using BetEcommerce.Repository.Product;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BetEcommerce.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartItemService _cartItemService;
        public CartService
            (
            IProductRepository productRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            ICartItemService cartItemService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _cartItemService = cartItemService;
            _cartItemRepository = cartItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddToCart(CartRequest cart)
        {
            var product = await _productRepository.GetByIdAsync(cart.ProductId);
            if (product == null)
                throw new HttpException(HttpStatusCode.NotFound, "Product not found");
            
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Identity.Name);
            int cartId = 0;

            var cartExist = _cartRepository.GetCartByUserId(userId);
            if(cartExist == null)
            {
                Cart cartAdd = new Cart();
                cartAdd.UserId = userId;
                _cartRepository.InsertAsync(cartAdd);
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
            var cartItems = _cartItemRepository.GetCartItemsListByCartId(cartId);
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
            var res = _cartRepository.GetCartByUserId(userId);
            if(res != null)
                cartId = res.Id;
               
            return cartId;
        }

        public async Task<bool> UpdateCart(CartRequest cart)
        {
            int cartId = GetCartId().Result;
            if(cartId > 0)
            {
                var cartItem = _cartItemRepository.GetCartItemByProductId(cartId, cart.ProductId);
                if (cartItem != null)
                {
                    var product = await _productRepository.GetByIdAsync(cart.ProductId);
                    if(product != null)
                    {
                        cartItem.quantity = cart.Quantity;
                        cartItem.UnitPrice = product.price;
                        cartItem.TotalPrice = product.price * cart.Quantity;
                        _cartItemRepository.UpdateAsync(cartItem);
                    }
                }
            }
            return true;
        }

        public async Task<bool> RemoveCartItem(int productId)
        {
            int cartId = GetCartId().Result;
            if (cartId > 0)
            {
                var cartItem = _cartItemRepository.GetCartItemByProductId(cartId, productId);
                if (cartItem != null)
                    await _cartItemRepository.DeleteAsync(cartItem);
            }
            return true;
        }
    }
}
