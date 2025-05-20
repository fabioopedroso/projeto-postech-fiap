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

        [HttpGet("ListLibraryGames")]
        [Authorize(Roles = "CommonUser")]
        public async Task<IActionResult> ListLibraryGames()
        {
            var games = await _userAppService.ListLibraryGamesAsync();
            return Ok(games);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto signature)
        {
            await _userAppService.Register(signature);
            return NoContent();
        }
    }
}
