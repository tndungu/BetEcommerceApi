using BetEcommerce.Model.API;
using BetEcommerce.Model.Request;
using BetEcommerce.Repository.Helpers;
using BetEcommerce.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BetEcommerce.Api.Controllers.API.V1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public Task<ApiResponse> Authenticate([FromBody] UserRequest userRequest)
        {
            try
            {
                return _userService.Authenticate(userRequest);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(HttpStatusCode.InternalServerError) { ResponseMessage = ex.Message };
                return Task.Run(() => response);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public Task<ApiResponse> Register(UserRequest userRequest)
        {
            try
            {
                return _userService.Register(userRequest);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(HttpStatusCode.InternalServerError) { ResponseMessage = ex.Message };
                return Task.Run(() => response);
            }

        }
    }
}
