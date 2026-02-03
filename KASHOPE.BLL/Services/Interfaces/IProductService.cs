

using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.ProductResponse;
using Microsoft.AspNetCore.Http;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface IProductService : IScopedService
    {
        Task<List<ProductResponse>> GetAllProductsAsync(HttpRequest request);
        Task<PagintedResponse<ProductUserResponse>> GetAllProductsForUserAsync(HttpRequest request, string lang = "en", int limit = 3, int page = 1, string? search = null, int? categoryId = null, decimal? MinPrice = null, decimal? MaxPrice = null , string? sortby = null, bool asc = true);
        Task<ProductDetailsUserResponse> GetProductDetailsForUserAsync(HttpRequest request, int productId, string lang = "en");
        Task CreateAsync(ProductRequest request);
        Task<BaseResponse> DeleteProduct(int id);
    }
}
