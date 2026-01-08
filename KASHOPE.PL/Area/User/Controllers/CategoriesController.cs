using KASHOPE.DAL.DATA;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;
using KASHOPE.BLL.Services.Classes;
using KASHOPE.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace KASHOPE.PL.Area.User.Controllers
{
    [Area("User")]
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
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
        public async Task<IActionResult> Index([FromQuery] string lang = "en")
        {
            var categories = await _categoryService.GetAllCategoriesAsync(lang);
            return Ok(new { message = _localizer["Success"].Value, categories });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id,[FromQuery]string lang = "en")
        {
            var category = await _categoryService.GetCategoryByIdAsync(id,lang);
            if (category is null)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }
            return Ok(new { message = _localizer["Success"].Value, category });
        }
    }
}
