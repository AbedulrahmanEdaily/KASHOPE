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
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public new async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductTranslations)
                .Include(p=>p.User)
                .Include(p=>p.SubImages)
                .Include(p=>p.Category)
                .ThenInclude(c => c.CategoryTranslations)
                .ToListAsync();
        }
        public  async Task<Product?> FindByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductTranslations)
                .Include(p => p.User)
                .Include(p => p.SubImages)
                .Include(p => p.Category)
                .ThenInclude(c => c.CategoryTranslations)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }
    }
}
