using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface IOrderRepository 
    {
        public Task CreateAsync(Order request);
        public Task<Order> GetBySessionIdAsync(string sesssionId);
        public Task UpdateAsync(Order order);
    }
}
