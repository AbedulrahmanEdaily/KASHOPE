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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category Request)
        {
            await _context.AddAsync(Request);
            await _context.SaveChangesAsync();
            return Request;
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category); 
            await _context.SaveChangesAsync();              
        }

        public async Task<Category?> FindbyIdAsync(int id)
        {
            return await _context.Categories.Include(c=>c.CategoryTranslations).FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories =  await _context.Categories.Include(c=>c.CategoryTranslations).Include(c=>c.User).ToListAsync();
            return categories;
        }

        public async Task UpdateAsync( Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            
        }
    }
}
