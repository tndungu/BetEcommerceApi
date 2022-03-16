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
    public class UserController : V1Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody] UserRequest userRequest)
        {
            try
            {
                var response = await _userService.Authenticate(userRequest);
                return Ok(new ApiResponse<UserResponse>().Success(response));
            }
            catch (Exception ex)
            {
                return betServerError(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(UserRequest userRequest)
        {
            try
            {
                var response = await _userService.Register(userRequest);
                return Ok(new ApiResponse<bool>().Success(response));
            }
            catch (Exception ex)
            {
                return betServerError(ex);
            }
        }
    }
}
