using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Repository.Repository.EF;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetEcommerce.Api.Controllers.API.V1
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : V1Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts([FromQuery] PointerParams @params)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _productService.GetProducts();
                    return Ok(new ApiResponse<List<Product>>().Success(response.Result));
                }
                return BadRequest(new ApiResponse<bool>().BadRequest(false));
            }
            catch(Exception ex)
            {
                return betServerError(ex);
            }
        }
    }
}
