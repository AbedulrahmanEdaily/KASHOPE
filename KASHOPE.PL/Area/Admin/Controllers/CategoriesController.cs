using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(ICategoryService categoryService, IStringLocalizer<SharedResource> localizer)
        {
            _categoryService = categoryService;
            _localizer = localizer;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(new { message = _localizer["Success"].Value, categories });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            return Ok(new { message = _localizer["Success"].Value, category });
        }
        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            _categoryService.CreateCategory(request);
            return Ok(new { message = _localizer["Success"].Value });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return Ok(new { message = _localizer["Success"].Value });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryRequest request)
        {
            var existingCategory = _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound(new { message = _localizer["CategoryNotFound"].Value });
            }
            _categoryService.UpdateCategory(id, request);
            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
