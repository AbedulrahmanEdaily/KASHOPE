using KASHOPE.DAL.DTO.Request.AccountRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.AccountResponse;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface IAuthenticationService : IScopedService
    {
        Task<BaseResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest request);
        Task<BaseResponse> LoginAsync(LoginRequest loginRequest);
        Task<bool> ConfirmEmailAsync(string token,string userId);
        Task<BaseResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<BaseResponse> ChangePasswordAsync(ChangePasswordRequest request);
        Task<LoginResponse> RefreshTokenAsync(TokenApiModel request);

    }
}
