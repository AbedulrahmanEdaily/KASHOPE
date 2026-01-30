using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DATA;
using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Classes
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItemRepository;

        public CheckoutService(ApplicationDbContext context,IProductRepository productRepository,ICartRepository cartRepository,IOrderRepository orderRepository,UserManager<ApplicationUser> userManager,IEmailSender emailSender,IOrderItemRepository orderItemRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _orderItemRepository = orderItemRepository;
        }

        
        public async Task<CheckoutResponse> HandlePaymentAsync(string sessionId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var service = new SessionService();
                var session = await service.GetAsync(sessionId);
                var userId = session.Metadata["userId"];
                var order = await _orderRepository.GetBySessionIdAsync(sessionId);
                order.PaymentId = session.PaymentIntentId;
                order.OrderStatus = OrderStatus.Approved;
                await _orderRepository.UpdateAsync(order);
                var user = await _userManager.FindByIdAsync(userId);
                var items = await _cartRepository.GetAllAsync(userId);
                var orderItems = new List<OrderItem>();
                var productUpdated = new List<(int productId, int quantity)>();
                foreach (var item in items)
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = item.ProductId,
                        OrderId = order.Id,
                        UnitPrice = item.Product.Price,
                        Quantity = item.Count,
                        TotalPrice = item.Count * item.Product.Price
                    };
                    orderItems.Add(orderItem);
                    productUpdated.Add((item.ProductId, item.Count));
                }
                await _orderItemRepository.CreateAsync(orderItems);
                var decrease = await _productRepository.DecreaseQuantityItemAsync(productUpdated);
                if (!decrease)
                {
                    await transaction.RollbackAsync();
                    return new CheckoutResponse
                    {
                        Success = false,
                        Message = "Not Enough Stock"
                    };
                }
                await _cartRepository.ClearAsync(userId);
                await transaction.CommitAsync();
                await _emailSender.SendEmailAsync(user.Email, "Payment Successfull", $"<h1>Thank you for your payment </h1> " +
                        $"<h5>Your payment for order : <strong>{order.Id}</strong></h5>" +
                        $"<h5>Total Amount : <strong>{order.AmountPaid}</strong></h5>");
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Payment Completed  Successfully"
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "Faild"
                };
            }
           
        }

        public async Task<CheckoutResponse> ProcessPaymentAsync(CheckoutRequest request,string userId , HttpRequest httpRequest)
        {
            var cartItems = await _cartRepository.GetAllAsync(userId);
            if (!cartItems.Any())
            {
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "User Does Not Have Item In Cart"
                };
            }
            decimal totalAmount = 0;
            foreach(var item in cartItems)
            {
                if(item.Product.Quantity < item.Count)
                {
                    return new CheckoutResponse
                    {
                        Success = false,
                        Message = "Not Enough Stock"
                    };
                }
                    totalAmount += item.Product.Price * item.Count;
            }
            Order order = new Order
            {
                UserId = userId,
                PaymentMethod = request.PaymentMethod,
                AmountPaid = totalAmount,
                PaymentStatus = PaymentStatus.UnPaid
            };
            if (request.PaymentMethod == PaymentMethod.Cash)
            {
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Done"
                };
            }
            else if(request.PaymentMethod == PaymentMethod.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/User/checkouts/success?session_Id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/checkouts/cancel",
                    Metadata = new Dictionary<string, string>
                    {
                        {"userId", userId }
                    }
                };
                foreach(var item in cartItems)
                {
                    options.LineItems.Add(
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.ProductTranslations.FirstOrDefault(t=>t.Language == "en")?.Name
                            },
                            UnitAmount = (long)item.Product.Price * 100,
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);
                order.SessionId = session.Id;
                order.PaymentStatus = PaymentStatus.Paid;
                await _orderRepository.CreateAsync(order);
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Done",
                    Url = session.Url
                };
            }
            else
            {
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Payment Method Not Supported"
                };
            }
        }
    }
}
