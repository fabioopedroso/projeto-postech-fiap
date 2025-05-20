using Application.DTOs.Auth.Signature;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthAppService _authAppService;

    public AuthController(IAuthAppService authAppService)
    {
        _authAppService = authAppService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto user)
    {
        try
        {
            var token = await _authAppService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}
