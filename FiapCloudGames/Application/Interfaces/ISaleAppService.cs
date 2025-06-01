using Application.DTOs.Sale.Result;
using Application.DTOs.Sale.Signature;

namespace Application.Interfaces;
public interface ISaleAppService
{
    Task<SaleDto> Create(CreateSaleDto dto);
    Task Update(UpdateSaleDto dto);
    Task<IEnumerable<SaleDto>> GetAll();
    Task<SaleDto> GetbyId(int id);
    Task AddGameToSale(UpdateSaleGameDto dto);
    Task RemoveGameFromSale(UpdateSaleGameDto dto);
    Task SetActiveStatusAsync(SetSaleActiveStatusDto dto);
}
