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

        public async Task<bool> DecreaseQuantityItemAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(i => i.productId);
            var products = await _context.Products.Where(p =>productIds.Contains(p.Id)).ToListAsync();
            foreach(var product in products)
            {
                var item = items.FirstOrDefault(p => p.productId == product.Id);
                if(product.Quantity < item.quantity)
                {
                    return false;
                }
                product.Quantity -= item.quantity;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
