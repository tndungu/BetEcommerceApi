﻿using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Model.Response;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ProductListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public ActionResult GetProducts([FromBody] PointerParams PointerParams)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = _productService.GetProducts(PointerParams);
                    return Ok(new ApiResponse<ProductListViewModel>().Success(response.Result));
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