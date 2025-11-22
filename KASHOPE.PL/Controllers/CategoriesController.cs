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

namespace KASHOPE.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(ApplicationDbContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        [HttpGet("")]
        public IActionResult index()
        {
            var categories = _context.Categories.Include(c => c.CategoryTranslations).ToList();
            var categoriesDTO = categories.Adapt<List<CategoryResponse>>();
            return Ok(new { message = _localizer["Success"].Value, categories = categoriesDTO });
        }
        [HttpPost("")]
        public IActionResult create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
