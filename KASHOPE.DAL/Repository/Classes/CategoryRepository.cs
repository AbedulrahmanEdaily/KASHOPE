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

        public Category Create(Category Request)
        {
            _context.Add(Request);
            _context.SaveChanges();
            return Request;
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category); 
                _context.SaveChanges();              
            }
        }
        public List<Category> GetAll()
        {
            var categories = _context.Categories.Include(c=>c.CategoryTranslations).ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            var category = _context.Categories.Include(c => c.CategoryTranslations).FirstOrDefault(c => c.Id == id);
            return category;
        }

        public void Update(int id, Category request)
        {
            var category = _context.Categories.Find(id);
            
            
                category.CategoryTranslations = request.CategoryTranslations;
                category.Status = request.Status;
                _context.SaveChanges();
            
        }
    }
}
