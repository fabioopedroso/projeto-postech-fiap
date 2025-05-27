using Application.DTOs.User.Results;
using Application.DTOs.User.Signatures;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : ControllerBase
    {
        private readonly IUserAdminAppService _userAdminAppService;
        

        public AdministratorController(IUserAdminAppService userAdminAppService)
        {
            _userAdminAppService = userAdminAppService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            await _userAdminAppService.CreateUser(dto);
            return Ok();
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userAdminAppService.GetUsers();
            return Ok(users);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userAdminAppService.GetUserById(id);
            return Ok(user);
        }

        [HttpPatch("PromoteUser/{id}")]
        public async Task<IActionResult> PromoteUser(int id)
        {
            await _userAdminAppService.PromoteUser(id);
            return Ok();
        }

        [HttpPatch("DemoteUser/{id}")]
        public async Task<IActionResult> DemoteUser(int id)
        {
            await _userAdminAppService.DemoteUser(id);
            return Ok();
        }

        [HttpPatch("SetUserActiveStatus")]
        public async Task<IActionResult> SetUserActiveStatus([FromBody] SetUserActiveStatusDto dto)
        {
            await _userAdminAppService.SetUserActiveStatus(dto);
            return Ok();
        }
    }
}