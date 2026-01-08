using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.CartRequest;
using KASHOPE.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace KASHOPE.PL.Area.User.Controllers
{
    [Area("User")]
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CartsController(ICartService cartService,IStringLocalizer<SharedResource> localizer)
        {
            _cartService = cartService;
            _localizer = localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddToCartAsync(userId, request);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                    return NotFound(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllProductsFromCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.GetAllProductsFromCart(userId);
            return Ok(new { message = _localizer["Success"].Value, result });
        }
        [HttpDelete("RemoveFromCart/{ProductId}")]
        public async Task<IActionResult> DeleteItemFromCart([FromRoute]int ProductId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.RemoveFromCartAsync(userId, ProductId);
            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                    return NotFound(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.ClearCartAsync(userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
