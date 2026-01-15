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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Cart>> GetAllAsync(string userId)
        {
            var carts = await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ThenInclude(p => p.ProductTranslations)
                .Include(c => c.User).ToListAsync();

            return carts;
        }
        public async Task CreateAsync(Cart request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cart request)
        {
             _context.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var carts = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();
            _context.RemoveRange(carts);
            await _context.SaveChangesAsync();
        }
    }
}
