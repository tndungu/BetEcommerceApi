using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetEcommerce.Api.Controllers.API.V1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : V1Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;

        public CartController(ICartService cartService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _cartItemService = cartItemService;
        }
        [HttpPost("AddToCart")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest Cart)
        {
            try
            {
                var response = await _cartService.AddToCart(Cart);
                return Ok(new ApiResponse<int>().Success(response));
            }
            catch (Exception ex)
            {
                return betServerError(ex);
            }
        }

        [HttpGet("GetCartItems")]
        [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartItems()
        {
            try
           { 
                var response = await _cartItemService.GetCartItems();
                return Ok(new ApiResponse<List<CartResponse>>().Success(response));
            }
            catch(Exception ex)
            {
                return betServerError(ex);
            }
        }
        [HttpGet("GetCartItemsCount")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartItemsCount()
        {
            try
            {
                var response = await _cartService.GetCartItemsCount();
                return Ok(new ApiResponse<int>().Success(response));
            }
            catch (Exception ex)
            {
                return betServerError(ex);
            }
        }

        [HttpPost("UpdateCart")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCart([FromBody] CartRequest Cart)
        {
            try
            {
                var response = await _cartService.UpdateCart(Cart);
                return Ok(new ApiResponse<bool>().Success(response));
            }
            catch (Exception ex)
            {
                return betServerError(ex);
            }
        }

        [HttpPost("RemoveFromCart")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromCart([FromBody] int productId)
        {
            try
            {
                var response = await _cartService.RemoveCartItem(productId);
                return Ok(new ApiResponse<bool>().Success(response));
            }
            catch (Exception ex)
            {
                return betServerError(ex);
            }
        }

    }
}
