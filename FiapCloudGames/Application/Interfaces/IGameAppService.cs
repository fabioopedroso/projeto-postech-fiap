using Application.DTOs.Game.Result;
using Application.DTOs.Game.Signature;

namespace Application.Interfaces;
public interface IGameAppService
{
    Task<GameDto> AddAsync(AddGameDto dto);
    Task UpdateAsync(UpdateGameDto dto);
    Task<IEnumerable<GameDto>> GetAllAsync();
    Task<GameDto> GetByIdAsync(int id);
    Task SetActiveStatusAsync(SetActiveStatusDto dto);
    Task SetPriceAsync(SetPriceDto dto);
}
