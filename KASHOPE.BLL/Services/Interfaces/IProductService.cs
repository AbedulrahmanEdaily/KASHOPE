

using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.DTO.Response.ProductResponse;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task CreateAsync(ProductRequest request);
    }
}
