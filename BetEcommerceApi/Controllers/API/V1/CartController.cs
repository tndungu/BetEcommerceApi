using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BetEcommerce.Api.Controllers.API.V1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : V1Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("AddToCart")]
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
        public async Task<IActionResult> GetCartItems()
        {
            try
           { 
                //var user = User.Identity.Name;
                var response = await _cartService.GetCartItems();
                return Ok(new ApiResponse<List<CartResponse>>().Success(response));
            }
            catch(Exception ex)
            {
                return betServerError(ex);
            }
        }
        
    }
}
