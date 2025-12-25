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
        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            var category =  request.Adapt<Category>();
            var createdCategory = await _categoryRepository.CreateAsync(category);
            return createdCategory.Adapt<CategoryResponse>();
        }

        public async Task<BaseResponse> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.FindbyIdAsync(id);
                if(category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category Not Found"
                    };
                }
                await _categoryRepository.DeleteAsync(category);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Category Removed Successfully"
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Con't Delete Category" + string.Join(',',ex)
                };
            }
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var response = categories.Adapt <List<CategoryResponse>>();
            return response;
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.FindbyIdAsync(id);
            var response = category.Adapt<CategoryResponse>();
            return response;
        }

        public async Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request)
        {
            var category = await _categoryRepository.FindbyIdAsync(id);
            if(category is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Category Not Found"
                };
            }
            if(request.CategoryTranslations != null)
            {
                foreach(var translation in request.CategoryTranslations)
                {
                    var existing = category.CategoryTranslations.FirstOrDefault(t => t.Language == translation.Language);
                    if(existing is not null)
                    {
                        existing.Name = translation.Name;
                    }
                    else
                    {
                        return new BaseResponse
                        {
                            Success = false,
                            Message = "Language Not Supported"
                        };
                    }
                }
            }
            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse
            {
                Success = true,
                Message = "Category Updated" 
            };
        }
        public async Task<BaseResponse> ToggleStatusAsync(int id)
        {
            var category = await _categoryRepository.FindbyIdAsync(id);
            if (category is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Category Not Found"
                };
            }
            category.Status = category.Status == Status.Inactive ? Status.Active : Status.Inactive;
            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse
            {
                Success = true,
                Message = "Status Toggled"
            };
        }

        public async Task<List<CategoryUserResponse>> GetAllCategoriesAsync(string lang)
        {
            var categories = await _categoryRepository.GetAllAsync();
            foreach(var category in categories)
            {
                category.CategoryTranslations = category.CategoryTranslations.Where(t => t.Language == lang).ToList();
            }
            var response = categories.BuildAdapter().AddParameters("lang",lang).AdaptToType<List<CategoryUserResponse>>();
            return response;
        }
    }
}
