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
        List<Category> GetAll();
        Category Create(Category Request);
        void Delete(int id);
        public Category GetById(int id);

        public void Update(int id, Category category);
    }
}
