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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task CreateAsync(Order request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetBySessionIdAsync(string sesssionId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.SessionId == sesssionId);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o=> o.User)
                .Include(o=>o.OrderItems)
                .ThenInclude(o=>o.Product)
                .FirstOrDefaultAsync(o => o.Id == id) ;
        }

        public async Task<List<Order>> GetOrderByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
                .Include(o=>o.User)
                .Where(o => o.OrderStatus == status)
                .ToListAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> HasUserDeliveredOrderForProductAsync(string userId, int productId)
        {
            return await _context.Orders.AnyAsync(o => o.UserId == userId && o.OrderItems.Any(oi=>oi.ProductId == productId) && o.OrderStatus == OrderStatus.Delivered);
        }
    }
}
