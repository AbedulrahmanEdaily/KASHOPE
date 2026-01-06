using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.Models;
using KASHOPE.PL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOPE.PL.Area.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[Area]/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ProductsController(IProductService productService, IStringLocalizer<SharedResource> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpPost()]
        public async Task<IActionResult> AddNewProduct([FromForm] ProductRequest requst)
        {
            await _productService.CreateAsync(requst);
            return Ok(new { message =  _localizer["Success"].Value });
        }
    }
}
