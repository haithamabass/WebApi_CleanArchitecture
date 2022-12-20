using Application.Dtos.Account;
using Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace CleanaArchitecture1.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthResponse _authService;

        public AuthController(IAuthResponse authService)
        {
            _authService = authService;
        }

        # region SetRefreshTokenInCookies

        private void SetRefreshTokenInCookies(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            //cookieOptionsExpires = DateTime.UtcNow.AddSeconds(cookieOptions.Timeout);

            Response.Cookies.Append("refreshTokenKey", refreshToken, cookieOptions);
        }

        #endregion

        #region SignUp Endpoint

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUpAsync([FromForm] SignUp model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var orgin = Request.Headers["origin"];
            var result = await _authService.SignUpAsync(model, orgin);

            if (!result.ISAuthenticated)
                return BadRequest(result.Message);

            //store the refresh token in a cookie
            SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        #endregion

        #region Login Endpoint

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.ISAuthenticated)
                return BadRequest(result.Message);

            //check if the user has a refresh token or not , to store it in a cookie
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        #endregion

        #region AssignRole Endpoint

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync(AssignRolesDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AssignRolesAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        #endregion

        #region RefreshTokenCheck Endpoint

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshTokenCheckAsync()
        {
            var refreshToken = Request.Cookies["refreshTokenKey"];

            var result = await _authService.RefreshTokenCheckAsync(refreshToken);

            if (!result.ISAuthenticated)
                return BadRequest(result);

            return Ok(result);
        }

        #endregion

        #region RevokeTokenAsync

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeTokenAsync(RevokeToken model)
        {
            var refreshToken = model.Token ?? Request.Cookies["refreshTokenKey"];

            //check if there is no token
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Token is required");

            var result = await _authService.RevokeTokenAsync(refreshToken);

            //check if there is a problem with "result"
            //if (!result)
            //    return BadRequest("Token is Invalid");

            return Ok("Done Revoke");
        }

        #endregion

        #region ConfirmEmail
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {

            return Ok(await _authService.ConfirmEmailAsync(userId, code));
        }
        #endregion
    }
}