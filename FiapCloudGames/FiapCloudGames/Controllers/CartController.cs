using Application.Contracts;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "CommonUser")]

public class CartController : ControllerBase
{
    private readonly ICartAppService _cartAppService;
    private readonly ICartCacheService _cartCacheService;
    private readonly ICurrentUseService _currentUseService;

    public CartController(ICartCacheService cartCacheService, ICartAppService cartAppService, ICurrentUseService currentUseService)
    {
        _cartCacheService = cartCacheService;
        _cartAppService = cartAppService;
        _currentUseService = currentUseService;
    }

    [HttpGet("Summary")]
    public async Task<IActionResult> GetCartSummary()
    {
        var cartSummary = await _cartCacheService.GetCachedCartSummaryAsync();
        return Ok(cartSummary);
    }

    [HttpPost("Games/{gameId}")]
    public async Task<IActionResult> AddGame(int gameId)
    {
        await _cartAppService.AddGame(gameId);
        _cartCacheService.InvalidateCartCache(_currentUseService.UserId);
        return Ok();
    }

    [HttpDelete("Games/{gameId}")]
    public async Task<IActionResult> RemoveGame(int gameId)
    {
        await _cartAppService.RemoveGame(gameId);
        _cartCacheService.InvalidateCartCache(_currentUseService.UserId);
        return Ok();
    }

    [HttpDelete("Games")]
    public async Task<IActionResult> ClearCart()
    {
        await _cartAppService.ClearCart();
        _cartCacheService.InvalidateCartCache(_currentUseService.UserId);
        return Ok();
    }
}

