using KASHOPE.DAL.DTO.Request.CartRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.CartResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface ICartService
    {
        Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request);
        Task<CartSummaryResponse> GetAllProductsFromCart(string userId);
        Task<BaseResponse> RemoveFromCartAsync(string userId , int productId);
        Task<BaseResponse> ClearCartAsync(string userId);
        Task<BaseResponse> UpdateQuantityAsync(string userId, int productId, UpdateQuantityRequest request);
    }
}
