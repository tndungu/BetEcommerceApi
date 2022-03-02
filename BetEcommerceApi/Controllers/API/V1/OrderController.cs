using BetEcommerce.Model.API;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetEcommerce.Api.Controllers.API.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : V1Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("Order")]
        public async Task<IActionResult> Order([FromBody] int userId)
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
