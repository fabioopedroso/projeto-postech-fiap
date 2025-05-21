using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "CommonUser")]

public class LibraryController : ControllerBase
{
    private readonly ILibraryAppService _libraryAppService;
    public LibraryController(ILibraryAppService libraryAppService)
    {
        _libraryAppService = libraryAppService;
    }

    [HttpGet("ListLibraryGames")]
    public async Task<IActionResult> ListLibraryGames()
    {
        var games = await _libraryAppService.ListLibraryGamesAsync();
        return Ok(games);
    }
}
