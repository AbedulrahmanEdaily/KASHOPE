using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.Models;
using KASHOPE.PL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOPE.PL.Area.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ProductsController(IProductService productService,IStringLocalizer<SharedResource> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }
        [HttpGet()]
        public async Task<IActionResult> Index([FromQuery] string? search = null, [FromQuery] string lang = "en", [FromQuery] int page = 1, [FromQuery] int limit = 3, [FromQuery] int? categoryId = null , [FromQuery] decimal? MinPrice = null, [FromQuery] decimal? MaxPrice = null , [FromQuery] string? sortby = null, [FromQuery] bool asc = true)
        {
            var results = await _productService.GetAllProductsForUserAsync(lang , limit , page , search , categoryId , MinPrice , MaxPrice , sortby , asc);
            return Ok(new { message = _localizer["Success"].Value, results });
        }
        [HttpGet("Details")]
        public async Task<IActionResult> GetAllProductsDetailsForUser([FromQuery] string lang = "en")
        {
            var results = await _productService.GetAllProductsDetailsForUserAsync(lang);
            return Ok(new { message = _localizer["Success"].Value, results });
        }
    }
}
