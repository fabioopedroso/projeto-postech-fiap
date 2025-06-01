using Application.DTOs.Sale.Result;
using Application.DTOs.Sale.Signature;
using Application.Exceptions;
using Application.Interfaces;
using Core.Entity;
using Core.Interfaces.Repository;
using Core.ValueObjects;

namespace Application.Services;
public class SaleAppService : ISaleAppService
{
    private readonly IUnitOfWork _unitOfWork;

    public SaleAppService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDto> Create(CreateSaleDto dto)
    {
        var sale = new Sale
        {
            Name = dto.Name,
            Description = dto.Description,
            Period = new DateRange(dto.StartDate, dto.EndDate), 
            DiscountPercentage = new DiscountPercentage(dto.DiscountPercentage)
        };

        await _unitOfWork.Sales.CreateAsync(sale);

        return new SaleDto
        {
            Id = sale.Id,
            CreationDate = sale.CreationDate,
            IsActive = sale.IsActive,
            Name = sale.Name,
            Description = sale.Description,
            StartDate = sale.Period.Start,
            DiscountPercentage = sale.DiscountPercentage.Value
        };
    }

    public async Task Update(UpdateSaleDto dto)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(dto.Id);
        sale.Description = dto.Description;
        sale.Period = new DateRange(dto.StartDate, dto.EndDate);
        sale.DiscountPercentage = new DiscountPercentage(dto.DiscountPercentage);
        await _unitOfWork.Sales.UpdateAsync(sale);
    }

    public async Task<IEnumerable<SaleDto>> GetAll()
    {
        var result = await _unitOfWork.Sales.GetAllAsync();
        
        return result.Select(sale => new SaleDto
            {
                Id = sale.Id,
                CreationDate = sale.CreationDate,
                IsActive = sale.IsActive,
                Name = sale.Name,
                Description = sale.Description,
                StartDate = sale.StartDate,
                EndDate = sale.EndDate,
                DiscountPercentage = sale.DiscountPercentage.Value
            }).ToList();
    }

    public async Task<SaleDto> GetbyId(int id)
    {
        var result = await _unitOfWork.Sales.GetByIdAsync(id);
        if (result == null)
            throw new NotFoundException("A promoção informada não foi localizada");

        return new SaleDto
        {
            Id = result.Id,
            CreationDate = result.CreationDate,
            IsActive = result.IsActive,
            Name = result.Name,
            Description = result.Description,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            DiscountPercentage = result.DiscountPercentage.Value
        };
    }

    public async Task AddGameToSale(UpdateSaleGameDto dto)
    {
        var sale = await _unitOfWork.Sales.GetSaleWithGamesByIdAsync(dto.SaleId);
        if (sale == null)
            throw new NotFoundException("A promoção informada não foi localizada");

        ValidateSale(sale);

        var game = await _unitOfWork.Games.GetGameWithActiveSalesByIdAsync(dto.GameId);
        if (game == null)
            throw new NotFoundException("O jogo informado não foi localizado");

        var isInAnotherActiveSale = game.Sales.Any(s => s.Id != sale.Id && s.IsActive);
        if (isInAnotherActiveSale)
            throw new InvalidOperationException("O jogo informado já está vinculado a uma promoção ativa.");

        if (sale.Games.Any(g => g.Id == game.Id))
            throw new InvalidOperationException("O jogo informado já está atribuído a essa promoção.");

        sale.Games.Add(game);
        await _unitOfWork.Sales.UpdateAsync(sale);
    }


    public async Task RemoveGameFromSale(UpdateSaleGameDto dto)
    {
        var sale = await _unitOfWork.Sales.GetSaleWithGamesByIdAsync(dto.SaleId);
        if (sale == null)
            throw new NotFoundException("A promoção informada não foi localizada");

        ValidateSale(sale);

        var game = await _unitOfWork.Games.GetByIdAsync(dto.GameId);

        if (!sale.Games.Any(g => g.Id == game.Id))
            throw new InvalidOperationException("O jogo informado não está atribuído a essa promoção.");

        sale.Games.Remove(game);
        await _unitOfWork.Sales.UpdateAsync(sale);
    }

    public async Task SetActiveStatusAsync(SetSaleActiveStatusDto dto)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(dto.Id);
        if (sale == null)
            throw new NotFoundException("A promoção informada não foi localizada.");
        
        sale.IsActive = dto.IsActive;
        await _unitOfWork.Sales.UpdateAsync(sale);
    }

    private async Task ValidateSale(Sale sale)
    {
        if (!sale.IsCurrentlyActive())
            throw new InvalidOperationException("A promoção não está ativa ou fora da validade.");
    }
}
