using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.UserRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.UserResponse;
using KASHOPE.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Classes
{
    public class ManageUsersService : IManageUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<UserRespnose>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = users.Adapt<List<UserRespnose>>();
            for(var i =0; i < users.Count; i++)
            {
                var role = await _userManager.GetRolesAsync(users[i]);
                result[i].Roles = role.ToList();
            }
            return result;
        }
        public Task<UserDetailsResponse> GetUserDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> BlockedUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }
            if(await _userManager.IsLockedOutAsync(user))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "User already Lockedout"
                };
            }
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user,DateTimeOffset.MaxValue);
            await _userManager.UpdateAsync(user);
            return new BaseResponse
            {
                Success = true,
                Message = "User Lockedout"
            };
        }

        public async Task<BaseResponse> UnBlockedUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }
            if (!await _userManager.IsLockedOutAsync(user))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "User already UnLockedout"
                };
            }
            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user, null);
            await _userManager.UpdateAsync(user);
            return new BaseResponse
            {
                Success = true,
                Message = "User UnLockedout"
            };
        }
        public async Task<BaseResponse> ChangeUserRoleAsync(ChangeUserRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.userId);
            if (user is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }
            var currentRole = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRole);
            await _userManager.AddToRoleAsync(user, request.Role);
            return new BaseResponse
            {
                Success = true,
                Message = "User Role Changed"
            };
        }

    }
}
