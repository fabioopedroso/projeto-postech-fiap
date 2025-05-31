using Application.Contracts;
using Application.DTOs.Cart.Result;
using Application.DTOs.Cart.Shared;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Core.Interfaces.Repository;

namespace Application.Services;
public class CartAppService : ICartAppService, ICartReadOnlyAppService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUseService _currentUserAppService;
    private readonly ICartCacheService _cartCacheService;

    public CartAppService(IUnitOfWork unitOfWork, ICurrentUseService currentUserAppService, ICartCacheService cartCacheService)
    {
        _unitOfWork = unitOfWork;
        _currentUserAppService = currentUserAppService;
        _cartCacheService = cartCacheService;
    }

    public async Task AddGame(int gameId)
    {
        var game = await _unitOfWork.Games.GetByIdAsync(gameId);
        if (game == null)
            throw new Exception("Jogo não encontrado.");

        await _unitOfWork.Carts.AddGameAsync(_currentUserAppService.UserId, game);
        await _unitOfWork.CommitAsync();

        _cartCacheService.InvalidateCartCache(_currentUserAppService.UserId);
    }
    public async Task RemoveGame(int gameId)
    {
        var game = await _unitOfWork.Games.GetByIdAsync(gameId);
        if (game == null)
            throw new Exception("Jogo não encontrado.");

        await _unitOfWork.Carts.RemoveGameAsync(_currentUserAppService.UserId, game);
        await _unitOfWork.CommitAsync();

        _cartCacheService.InvalidateCartCache(_currentUserAppService.UserId);
    }

    public async Task ClearCart()
    {
        await _unitOfWork.Carts.ClearCartAsync(_currentUserAppService.UserId);
        
        _cartCacheService.InvalidateCartCache(_currentUserAppService.UserId);
    }

    public async Task<CartSummaryDto> GetCartSummary()
    {
        var games = await GetAllGames();
        var totalPrice = await GetTotalPrice();
        var itemCount = await GetItemCount();

        return new CartSummaryDto
        {
            TotalPrice = totalPrice,
            ItemCount = itemCount,
            Games = games
        };
    }

    public async Task<List<CartGameData>> GetAllGames()
    {
        var games = await _unitOfWork.Carts.GetGamesByUserIdAsync(_currentUserAppService.UserId);

        return games.Select(g => new CartGameData
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            Genre = g.Genre,
            Price = g.Price
        }).ToList();
    }

    public Task<int> GetItemCount()
    {
        return _unitOfWork.Carts.GetItemCountAsync(_currentUserAppService.UserId);
    }

    public Task<decimal> GetTotalPrice()
    {
        return _unitOfWork.Carts.GetTotalPriceAsync(_currentUserAppService.UserId);
    }
}
