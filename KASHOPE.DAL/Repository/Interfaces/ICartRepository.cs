using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface ICartRepository : IScopedRepository
    {
        Task<List<Cart>> GetAllAsync(string userId);
        Task CreateAsync(Cart request);
        Task UpdateAsync(Cart item);
        Task DeleteAsync(Cart request);
        Task ClearAsync(string userId);
        Task<Cart?> GetCartItem(string userId, int productId);
    }
}
