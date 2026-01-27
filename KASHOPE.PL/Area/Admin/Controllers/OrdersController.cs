using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.OrderRequest;
using KASHOPE.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOPE.PL.Area.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetOrderByStatus([FromQuery] OrderStatus status = OrderStatus.Pending)
        {
            var result = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(result);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int orderId )
        {
            var result = await _orderService.GetOrderByIdAsync(orderId);
            if (result is null)
                return NotFound(new {message = "Order not found"});
            return Ok(result);
        }
        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateOrderStatys([FromBody] UpdateOrderStatusRequest request , [FromRoute] int orderId)
        {
            var result = await _orderService.UpdateOrderStatus(orderId , request.OrderStatus);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
