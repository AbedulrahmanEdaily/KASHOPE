using KASHOPE.DAL.DTO.Request.CategoryRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.CategoryResponse;
using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface ICategoryService : IScopedService
    {
        public Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request);
        public Task<List<CategoryResponse>> GetAllCategoriesAsync();
        public Task<List<CategoryUserResponse>> GetAllCategoriesAsync(string lang);
        public Task<BaseResponse> DeleteCategoryAsync(int id);
        public Task<CategoryResponse> GetCategoryByIdAsync(int id);
        Task<CategoryUserResponse> GetCategoryByIdAsync(int id, string lang);
        public Task<BaseResponse> UpdateCategoryAsync(int id , CategoryRequest request);
        public Task<BaseResponse> ToggleStatusAsync(int id);
    }
}
