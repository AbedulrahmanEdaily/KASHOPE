using KASHOPE.DAL.DATA;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Repository.Classes
{
    public class CategoryRepository :GenericRepository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public new async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.Categories.Include(c=>c.CategoryTranslations).Include(c => c.User).FirstOrDefaultAsync(c=>c.Id == id);
        }

        public new async Task<List<Category>> GetAllAsync()
        {
            var categories =  await _context.Categories.Include(c=>c.CategoryTranslations).Include(c=>c.User).ToListAsync();
            return categories;
        }

    }
}
