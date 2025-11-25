using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.Repository.Classes;
using KASHOPE.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }
        public CategoryResponse CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            var createdCategory = _categoryRepository.Create(category).Adapt<CategoryResponse>();
            return createdCategory;
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id); 
        }

        public List<CategoryResponse> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            var response = categories.Adapt <List<CategoryResponse>>();
            return response;
        }

        public CategoryResponse GetCategoryById(int id)
        {
            var category = _categoryRepository.GetById(id);
            var response = category.Adapt<CategoryResponse>();
            return response;
        }

        public void UpdateCategory(int id, CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _categoryRepository.Update(id, category);
        }
    }
}
