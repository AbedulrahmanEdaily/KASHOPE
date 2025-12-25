using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.PL.Area.Identity
{
    [Area("auth")]
    [Route("api/[Area]/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            var result = await _authenticationService.ConfirmEmailAsync(token, userId);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            var result = await _authenticationService.ResetPasswordAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordRequest request)
        {
            var result = await _authenticationService.ChangePasswordAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
