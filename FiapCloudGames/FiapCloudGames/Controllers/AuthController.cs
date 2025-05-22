using Application.Contracts;
using Application.DTOs.Auth.Signature;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authAppService;

    public AuthController(IAuthService authAppService)
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
