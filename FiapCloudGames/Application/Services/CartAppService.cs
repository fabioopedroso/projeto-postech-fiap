﻿using Application.Contracts;
using Application.DTOs.Cart.Result;
using Application.DTOs.Cart.Shared;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Core.Interfaces.Repository;

namespace Application.Services;
public class CartAppService : ICartAppService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUseService _currentUserAppService;

    public CartAppService(IUnitOfWork unitOfWork, ICurrentUseService currentUserAppService)
    {
        _unitOfWork = unitOfWork;
        _currentUserAppService = currentUserAppService;
    }

    public async Task AddGame(int gameId)
    {
        var game = await _unitOfWork.Games.GetByIdAsync(gameId);
        if (game == null)
            throw new NotFoundException("Jogo não encontrado.");

        await _unitOfWork.Carts.AddGameAsync(_currentUserAppService.UserId, game);
        await _unitOfWork.CommitAsync();
    }
    public async Task RemoveGame(int gameId)
    {
        var game = await _unitOfWork.Games.GetByIdAsync(gameId);
        if (game == null)
            throw new NotFoundException("Jogo não encontrado.");

        await _unitOfWork.Carts.RemoveGameAsync(_currentUserAppService.UserId, game);
        await _unitOfWork.CommitAsync();
    }

    public async Task ClearCart()
    {
        await _unitOfWork.Carts.ClearCartAsync(_currentUserAppService.UserId);
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

    private async Task<List<CartGameData>> GetAllGames()
    {
        var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(_currentUserAppService.UserId);

        return cart.Games.Select(g => new CartGameData
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            Genre = g.Genre,
            Price = g.Amount,
            DiscountedPrice = g.GetDiscountedPrice()
        }).ToList();
    }

    private Task<int> GetItemCount()
    {
        return _unitOfWork.Carts.GetItemCountAsync(_currentUserAppService.UserId);
    }

    private Task<decimal> GetTotalPrice()
    {
        return _unitOfWork.Carts.GetTotalPriceAsync(_currentUserAppService.UserId);
    }
}
