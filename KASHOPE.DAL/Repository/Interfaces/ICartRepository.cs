using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllAsync(string userId);
        Task CreateAsync(Cart request);
        Task IncreaseCountOfItemAsync(Cart item,int count);
        Task DeleteAsync(Cart request);
        Task ClearAsync(string userId);
    }
}
