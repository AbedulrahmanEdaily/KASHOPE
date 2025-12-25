using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category Request);
        Task DeleteAsync(Category category);
        Task UpdateAsync(Category category);
        Task <Category> FindbyIdAsync(int id);
    }
}
