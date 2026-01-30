using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.ReviewRequest;
using KASHOPE.DAL.Models;
using KASHOPE.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace KASHOPE.PL.Area.User.Controllers
{
    [Area("User")]
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IReviewService _reviewService;

        public ProductsController(IProductService productService,IStringLocalizer<SharedResource> localizer,IReviewService reviewService)
        {
            _productService = productService;
            _localizer = localizer;
            _reviewService = reviewService;
        }
        [HttpGet()]
        public async Task<IActionResult> Index([FromQuery] string? search = null, [FromQuery] string lang = "en", [FromQuery] int page = 1, [FromQuery] int limit = 3, [FromQuery] int? categoryId = null , [FromQuery] decimal? MinPrice = null, [FromQuery] decimal? MaxPrice = null , [FromQuery] string? sortby = null, [FromQuery] bool asc = true)
        {
            var results = await _productService.GetAllProductsForUserAsync(Request , lang , limit , page , search , categoryId , MinPrice , MaxPrice , sortby , asc);
            return Ok(new { message = _localizer["Success"].Value, results });
        }
        [HttpGet("Details/{productId}")]
        public async Task<IActionResult> GetAllProductsDetailsForUser([FromRoute] int productId,[FromQuery] string lang = "en")
        {
            var results = await _productService.GetProductDetailsForUserAsync(Request ,productId,lang);
            return Ok(new { message = _localizer["Success"].Value, results });
        }
        [HttpPost("{proudctId}/Review")]
        public async Task<IActionResult> AddReview([FromRoute] int proudctId , [FromBody] ReviewRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var result = await _reviewService.AddReviewAsync(userId , proudctId , request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
