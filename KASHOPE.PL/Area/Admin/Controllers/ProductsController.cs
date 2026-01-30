using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.Models;
using KASHOPE.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOPE.PL.Area.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
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
            var products = await _productService.GetAllProductsAsync(Request);
            return Ok(products);
        }
        [HttpPost()]
        public async Task<IActionResult> AddNewProduct([FromForm] ProductRequest request)
        {
            await _productService.CreateAsync(request);
            return Created();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
