using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface IOrderRepository : IScopedRepository
    {
        public Task CreateAsync(Order request);
        public Task<Order?> GetBySessionIdAsync(string sesssionId);
        public Task UpdateAsync(Order order);
        public Task<List<Order>> GetOrderByStatusAsync(OrderStatus status);
        public Task<Order?> GetOrderByIdAsync(int id);
        Task<bool> HasUserDeliveredOrderForProductAsync(string userId, int productId);
    }
}
