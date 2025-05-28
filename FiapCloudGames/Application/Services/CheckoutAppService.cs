using Application.Contracts;
using Application.DTOs.Cart.Shared;
using Application.DTOs.Checkout.Result;
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

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var cartGames = await _unitOfWork.Carts.GetGamesByUserIdAsync(userId);
            var totalPrice = await _unitOfWork.Carts.GetTotalPriceAsync(userId);

            if (!cartGames.Any())
                throw new InvalidOperationException("Seu carrinho está vazio.");

            await _unitOfWork.Libraries.AddGames(userId, cartGames);
            await _unitOfWork.Carts.ClearCartAsync(userId);

            await _unitOfWork.CommitAsync();

            _libraryCacheService.InvalidateLibraryGamesCache(userId);

            return new OrderResultDto
            {
                UserId = userId,
                UserName = userName,
                Games = cartGames.Select(g => new PurchasedGameDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Price = g.Price
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
