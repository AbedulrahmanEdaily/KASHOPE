using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.UserRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOPE.PL.Area.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IManageUsersService _manageUsersService;

        public UsersController(IManageUsersService manageUsersService)
        {
            _manageUsersService = manageUsersService;
        }
        [HttpGet()]
        public async Task<IActionResult> Index() => Ok(await _manageUsersService.GetUsersAsync());
        [HttpPatch("BlockUser/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId)
        {
            var result = await _manageUsersService.BlockedUserAsync(userId);
            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                    return NotFound(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPatch("UnBlockUser/{userId}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        {
            var result = await _manageUsersService.UnBlockedUserAsync(userId);
            if (!result.Success)
            {
                if(result.Message.Contains("Not Found"))
                    return NotFound(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPatch("ChangeUserRole")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangeUserRole(ChangeUserRoleRequest request)
        {
            var result = await _manageUsersService.ChangeUserRoleAsync(request);
            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                    return NotFound(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
