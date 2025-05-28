using Application.Interfaces;
using Application.Interfaces.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "CommonUser")]

public class LibraryController : ControllerBase
{
    private readonly ILibraryCacheService _libraryCacheService;

    public LibraryController(ILibraryCacheService libraryCacheService)
    {
        _libraryCacheService = libraryCacheService;
    }

    [HttpGet("ListLibraryGames")]
    public async Task<IActionResult> ListLibraryGames()
    {
        var games = await _libraryCacheService.GetCachedLibraryGamesAsync();
        return Ok(games);
    }
}
