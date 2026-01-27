using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.OrderResponse;
using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetOrdersByStatusAsync(OrderStatus status);
        Task<BaseResponse> UpdateOrderStatus(int orderId, OrderStatus newOrderStatus);
        Task<OrderResponse?> GetOrderByIdAsync(int orderId);
    }
}
