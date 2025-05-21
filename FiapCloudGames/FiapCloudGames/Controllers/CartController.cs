using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "CommonUser")]

public class CartController : ControllerBase
{
    private readonly ICartAppService _cartAppService;
    
    public CartController(ICartAppService cartAppService)
    {
        _cartAppService = cartAppService;
    }

    [HttpGet("Summary")]
    public async Task<IActionResult> GetCartSummary()
    {
        var cartSummary = await _cartAppService.GetCartSummary();
        return Ok(cartSummary);
    }

    [HttpGet("Games")]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await _cartAppService.GetAllGames();
        return Ok(games);
    }

    [HttpGet("TotalPrice")]
    public async Task<IActionResult> GetTotalPrice()
    {
        var totalPrice = await _cartAppService.GetTotalPrice();
        return Ok(totalPrice);
    }

    [HttpGet("ItemCount")]
    public async Task<IActionResult> GetItemCount()
    {
        var itemCount = await _cartAppService.GetItemCount();
        return Ok(itemCount);
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

