using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<CheckoutResponse> ProccessPaymentAsync(CheckoutRequest request , string userId);
    }
}
