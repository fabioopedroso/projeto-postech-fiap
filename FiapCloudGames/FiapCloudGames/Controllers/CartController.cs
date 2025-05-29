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

    public CartController(ICartCacheService cartCacheService, ICartAppService cartAppService)
    {
        _cartCacheService = cartCacheService;
        _cartAppService = cartAppService;
    }

    [HttpGet("Summary")]
    public async Task<IActionResult> GetCartSummary()
    {
        var cartSummary = await _cartCacheService.GetCachedCartSummaryAsync();
        return Ok(cartSummary);
    }

    [HttpGet("Games")]
    public async Task<IActionResult> GetAllGames()
    {
        var cartSummary = await _cartCacheService.GetCachedCartSummaryAsync();
        return Ok(cartSummary.Games);
    }

    [HttpGet("TotalPrice")]
    public async Task<IActionResult> GetTotalPrice()
    {
        var cartSummary = await _cartCacheService.GetCachedCartSummaryAsync();
        return Ok(cartSummary.TotalPrice);
    }

    [HttpGet("ItemCount")]
    public async Task<IActionResult> GetItemCount()
    {
        var cartSummary = await _cartCacheService.GetCachedCartSummaryAsync();
        return Ok(cartSummary.ItemCount);
    }

    [HttpPost("Games/{gameId}")]
    public async Task<IActionResult> AddGame(int gameId)
    {
        await _cartAppService.AddGame(gameId);
        return Ok();
    }

    [HttpDelete("Games/{gameId}")]
    public async Task<IActionResult> RemoveGame(int gameId)
    {
        await _cartAppService.RemoveGame(gameId);
        return Ok();
    }

    [HttpDelete("Games")]
    public async Task<IActionResult> ClearCart()
    {
        await _cartAppService.ClearCart();
        return Ok();
    }
}

