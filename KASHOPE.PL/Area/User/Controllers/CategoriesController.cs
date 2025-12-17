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
    //[Authorize(Roles ="User")]
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
    }
}
