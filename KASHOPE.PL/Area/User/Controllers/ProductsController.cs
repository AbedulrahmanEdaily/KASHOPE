using KASHOPE.BLL.Services.Interfaces;
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
        public async Task<IActionResult> Index([FromQuery] string lang = "en")
        {
            var results = await _productService.GetAllProductsForUserAsync(lang);
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
