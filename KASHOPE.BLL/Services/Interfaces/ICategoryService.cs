using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        public CategoryResponse CreateCategory(CategoryRequest request);
        public List<CategoryResponse> GetAllCategories();
        public void DeleteCategory(int id);
        public CategoryResponse GetCategoryById(int id);
        public void UpdateCategory(int id , CategoryRequest request);
    }
}
