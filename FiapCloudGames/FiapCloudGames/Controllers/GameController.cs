using Application.DTOs.Game.Signature;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GameController : ControllerBase
{
    private readonly IGameAppService _gameAppService;
    private readonly IGameCacheService _gameCacheService;

    public GameController(IGameCacheService gameCacheService, IGameAppService gameAppService)
    {
        _gameCacheService = gameCacheService;
        _gameAppService = gameAppService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator,CommonUser")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _gameCacheService.GetCachedGamesAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator,CommonUser")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _gameCacheService.GetCachedGameByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Add([FromBody] AddGameDto dto)
    {
        var result = await _gameAppService.AddAsync(dto);
        _gameCacheService.InvalidateGamesCache();
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGameDto dto)
    {
        dto.Id = id;
        await _gameAppService.UpdateAsync(dto);
        _gameCacheService.InvalidateGamesCache();
        _gameCacheService.InvalidateGameCache(dto.Id);
        return NoContent();
    }

    [HttpPatch("SetActiveStatus")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> SetActiveStatus([FromBody] SetGameActiveStatusDto dto)
    {
        await _gameAppService.SetActiveStatusAsync(dto);
        _gameCacheService.InvalidateGamesCache();
        _gameCacheService.InvalidateGameCache(dto.Id);
        return NoContent();
    }

    [HttpPatch("SetPrice")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> SetPrice([FromBody] SetPriceDto dto)
    {
        await _gameAppService.SetPriceAsync(dto);
        _gameCacheService.InvalidateGamesCache();
        _gameCacheService.InvalidateGameCache(dto.Id);
        return NoContent();
    }
}
