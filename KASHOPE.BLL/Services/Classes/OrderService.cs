using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.OrderResponse;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderResponse?> GetOrderByIdAsync(int orderId)
        {
            var orders = await _orderRepository.GetOrderByIdAsync(orderId);
            var result = orders.Adapt<OrderResponse>();
            return result;
        }

        public async Task<List<OrderResponse>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var orders = await _orderRepository.GetOrderByStatusAsync(status);
            var result = orders.Adapt<List<OrderResponse>>();
            return result;
        }

        public async Task<BaseResponse> UpdateOrderStatus(int orderId, OrderStatus newOrderStatus)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if(order is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Order Not Found"
                };
            }
            order.OrderStatus = newOrderStatus;
            if(newOrderStatus == OrderStatus.Delivered)
            {
                order.PaymentStatus = PaymentStatus.Paid;
            }
            await _orderRepository.UpdateAsync(order);
            return new BaseResponse
            {
                Success = true,
                Message = "Order status updated"
            };
        }
    }
}
