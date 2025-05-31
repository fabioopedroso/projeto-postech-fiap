using Application.DTOs.Game.Result;

namespace Application.Interfaces
{
    public interface IGameReadOnlyAppService
    {
        Task<IEnumerable<GameDto>> GetAllAsync();
        Task<GameDto> GetByIdAsync(int id);
    }
}