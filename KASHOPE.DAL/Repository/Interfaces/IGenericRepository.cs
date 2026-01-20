using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T request);
        Task DeleteAsync(T request);
        Task UpdateAsync(T request);
        Task<T?> FindByIdAsync(int id);
    }
}
