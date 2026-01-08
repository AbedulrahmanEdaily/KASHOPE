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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
     
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task DeleteAsync(T request)
        {
            _context.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> FindbyIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await  _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T request)
        {
            _context.Set<T>().Update(request);
            await _context.SaveChangesAsync();

        }
    }
}
