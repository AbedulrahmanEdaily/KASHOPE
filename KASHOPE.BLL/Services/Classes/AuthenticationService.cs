using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.AccountRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.AccountResponse;
using KASHOPE.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace KASHOPE.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IEmailSender emailSender,IConfiguration configuration,SignInManager<ApplicationUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<BaseResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Invalid Email"
                    };
                }
                else if (await _userManager.IsLockedOutAsync(user))
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "User is locked out,try again later"
                    };
                }
                var password = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password,true);
                if(password.IsLockedOut)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "User is locked out,try again later"
                    };
                }else if(password.IsNotAllowed)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Email is not confirmed"
                    };
                } else if (!password.Succeeded)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Invalid Password"
                    };
                }
                var refreshToken = await _tokenService.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                user.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(user);
                return new LoginResponse
                {
                    Success = true,
                    Message = "Login Successfully",
                    Token = await _tokenService.GenerateJwtToken(user),
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "An error occurred during login: " + ex.Message
                };
            }
        }

        public async Task<BaseResponse> RegisterAsync(RegisterRequest registerRequest,HttpRequest request)
        {
            try
            {
                var user = registerRequest.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                if (!result.Succeeded)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
                    };
                }
                await _userManager.AddToRoleAsync(user, "User");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);
                var url = $"{request.Scheme}://{request.Host}/api/auth/account/ConfirmEmail?token={token}&userId={user.Id}"; ;
                await _emailSender.SendEmailAsync(user.Email, "Welcome to KASHOPE", $"<p>Thank you for registering {user.FullName} Please Confirm Your Email by</p>"+$"<a href='{url}'>Click Here</a>");
                return new BaseResponse
                {
                    Success = true,
                    Message = "User registered successfully"
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "An error occurred during registration: " + ex.Message
                   
                };
            }
        }
        public async Task<bool> ConfirmEmailAsync(string token,string userId) { 
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return false;
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }
       

        public async Task<BaseResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await  _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Invalid Email"
                };
            }
            var code = Guid.NewGuid().ToString().Substring(0,4).ToUpper();
            user.CodeResetPassword = code;
            user.CodeResetPasswordExpiration = DateTime.UtcNow.AddMinutes(5);

            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email, "Reset Password Code", $"<p>Your password reset code is: <strong>{code}</strong></p><p>This code will expire in 5 minutes.</p>");
            return new BaseResponse
            {
                Success = true,
                Message = "Reset password code sent to email"
            };
        }

        public async Task<BaseResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Invalid Email"
                };
            }
            else if (user.CodeResetPassword != request.CodeResetPassword || user.CodeResetPasswordExpiration < DateTime.UtcNow)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Invalid or expired reset code"
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Password reset failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
            user.CodeResetPassword = null;
            user.CodeResetPasswordExpiration = null;
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email, "Password Changed", "<p>Your password is Changed</p>");
            return new BaseResponse
            {
                Success = true,
                Message = "Password changed successfully"
            };
        }
        public async Task<LoginResponse> RefreshTokenAsync(TokenApiModel request)
        {

            string accessToken = request.AccessToken;
            string refreshToken = request.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid Refresh Token"
                };
            }
                
            var newAccessToken = await _tokenService.GenerateJwtToken(user);
            var newRefreshToken = await _tokenService.GenerateRefreshToken();
            user.RefreshToken =  newRefreshToken;
            await _userManager.UpdateAsync(user);
            return new LoginResponse
            {
                Success = true,
                Message = "Token Refreshed Successfully",
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
