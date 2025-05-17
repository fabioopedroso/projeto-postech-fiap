using Application.DTOs.Signatures;
using Application.Interfaces;
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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserSignature signature)
        {
            var result = await _userAppService.CreateUser(signature);
            if (result)
            {
                return Ok("User created successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }
    }
}
