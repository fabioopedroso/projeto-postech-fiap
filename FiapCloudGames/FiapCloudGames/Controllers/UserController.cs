using Application.DTOs.User.Signatures;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _userAppService.Register(dto);
            return NoContent();
        }

        [HttpPost("ChangePassword")]
        [Authorize(Roles = "Administrator,CommonUser")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            await _userAppService.ChangePassword(dto);
            return NoContent();
        }

        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "Administrator,CommonUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto dto)
        {
            await _userAppService.DeleteUser(dto);
            return NoContent();
        }
    }
}
