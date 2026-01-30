using KASHOPE.DAL.DTO.Request.UserRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.UserResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface IManageUsersService
    {
        Task<List<UserRespnose>> GetUsersAsync();
        Task<UserDetailsResponse> GetUserDetailsAsync();
        Task<BaseResponse> BlockedUserAsync(string userId);
        Task<BaseResponse> UnBlockedUserAsync(string userId);
        Task<BaseResponse> ChangeUserRoleAsync(ChangeUserRoleRequest request);

    }
}
