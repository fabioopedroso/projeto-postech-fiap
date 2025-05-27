using Application.DTOs.Game.Signature;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Administrator")]

public class GameController : ControllerBase
{
    private readonly IGameAppService _gameAppService;

    public GameController(IGameAppService gameAppService)
    {
        _gameAppService = gameAppService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator,CommonUser")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _gameAppService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator,CommonUser")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _gameAppService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Add([FromBody] AddGameDto dto)
    {
        var result = await _gameAppService.AddAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGameDto dto)
    {
        dto.Id = id;
        await _gameAppService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpPatch("SetActiveStatus")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> SetActiveStatus([FromBody] SetActiveStatusDto dto)
    {
        await _gameAppService.SetActiveStatusAsync(dto);
        return NoContent();
    }

    [HttpPatch("SetPrice")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> SetPrice([FromBody] SetPriceDto dto)
    {
        await _gameAppService.SetPriceAsync(dto);
        return NoContent();
    }
}
