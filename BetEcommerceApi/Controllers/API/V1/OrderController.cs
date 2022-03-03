using BetEcommerce.Model.API;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetEcommerce.Api.Controllers.API.V1
{

    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : V1Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("Order")]
        public async Task<IActionResult> Order([FromQuery] int userId)
        {
            try
            {
                var response = await _orderService.Order(userId);
                return Ok(new ApiResponse<bool>().Success(response));
            }
            catch (Exception ex)
            {
                return betServerError(ex);
            }
        }
    }
}
