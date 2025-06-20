﻿using Application.Contracts;
using Application.DTOs.Cart.Shared;
using Application.DTOs.Checkout.Result;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Core.Interfaces.Repository;

namespace Application.Services;
public class CheckoutAppService : ICheckoutAppService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUseService _currentUserAppService;
    private readonly ILibraryCacheService _libraryCacheService;

    public CheckoutAppService(IUnitOfWork unitOfWork, ICurrentUseService currentUserAppService, ILibraryCacheService libraryCacheService)
    {
        _unitOfWork = unitOfWork;
        _currentUserAppService = currentUserAppService;
        _libraryCacheService = libraryCacheService;
    }

    public async Task<OrderResultDto> CheckoutCart()
    {
        var userId = _currentUserAppService.UserId;
        var userName = _currentUserAppService.UserName;
        var email = _currentUserAppService.Email;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            var cartGames = cart.Games.ToList();
            var totalPrice = await _unitOfWork.Carts.GetTotalPriceAsync(userId);

            if (!cartGames.Any())
                throw new BusinessException("Seu carrinho está vazio.");

            await _unitOfWork.Libraries.AddGames(userId, cartGames);
            await _unitOfWork.Carts.ClearCartAsync(userId);

            await _unitOfWork.CommitAsync();

            _libraryCacheService.InvalidateLibraryGamesCache(userId);

            return new OrderResultDto
            {
                UserId = userId,
                UserName = userName,
                Email = email,
                Games = cartGames.Select(g => new PurchasedGameDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Price = g.GetDiscountedPrice()
                }).ToList(),
                TotalPrice = totalPrice
            };
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new ApplicationException("Ocorreu um erro ao finalizar o pedido.", ex);
        }
    }
}
