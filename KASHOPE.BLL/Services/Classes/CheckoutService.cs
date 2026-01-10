using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.Repository.Interfaces;
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
        private readonly ICartRepository _cartRepository;

        public CheckoutService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CheckoutResponse> ProccessPaymentAsync(CheckoutRequest request,string userId)
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
                if(item.Product.Quantity > item.Count)
                {
                    return new CheckoutResponse
                    {
                        Success = false,
                        Message = "Not Enough Stock"
                    };
                }
                    totalAmount += item.Product.Price * item.Count;
            }
            if(request.PaymentMethod.ToLower() == "cash")
            {
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "Done"
                };
            }
            else if(request.PaymentMethod.ToLower() == "visa")
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"https://localhost:7026/checkout/success",
                    CancelUrl = $"https://localhost:7026/checkout/cancel",
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
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);
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
