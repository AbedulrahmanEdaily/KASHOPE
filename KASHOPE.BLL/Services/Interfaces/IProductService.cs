

using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.ProductResponse;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<PagintedResponse<ProductUserResponse>> GetAllProductsForUserAsync(string lang = "en", int limit = 3, int page = 1, string? search = null, int? categoryId = null, decimal? MinPrice = null, decimal? MaxPrice = null , string? sortby = null, bool asc = true);
        Task<List<ProductDetailsUserResponse>> GetAllProductsDetailsForUserAsync(string lang);
        Task CreateAsync(ProductRequest request);
        Task<BaseResponse> DeleteProduct(int id);
    }
}
