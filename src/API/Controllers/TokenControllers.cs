using Dotby.API.ActionFilters;
using Dotby.Application.DTOs;
using Dotby.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Dotby.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TokenController(IServiceManager service)
        {
            _service = service;
        }
        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh()
        {
            HttpContext.Request.Cookies.TryGetValue("accessToken", out var accessToken);
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Missing authentication tokens");
            }
            var tokenDto = new TokenDto(accessToken, refreshToken);
            var tokenDtoReturn = await _service.AuthenticationService.RefreshToken(tokenDto);

            _service.AuthenticationService.SetTokenInsideCookie(tokenDtoReturn, HttpContext);

            return Ok();
        }
    }
}