using BetEcommerce.Model.API;
using BetEcommerce.Repository.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetEcommerce.Api.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult betServerError(Exception ex)
        {
            try
            {
                if (ex is HttpException)
                {
                    var _exception = (HttpException)ex;
                    return StatusCode(_exception.StatusCode, new ApiResponse<bool>().BadRequest(false, _exception.Message));
                }
                return StatusCode(500, new ApiResponse<bool>().BadRequest(false, ex.Message));
            }
            catch
            {
                return StatusCode(500, new ApiResponse<bool>().BadRequest(false, ex.Message));
            }
        }
    }
}
