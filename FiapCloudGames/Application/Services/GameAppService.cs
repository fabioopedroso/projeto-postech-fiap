using Application.DTOs.Game.Result;
using Application.DTOs.Game.Signature;
using Application.Interfaces;
using Core.Entity;
using Core.Interfaces.Repository;
using Core.ValueObjects;

namespace Application.Services;
public class GameAppService : IGameAppService
{
    private readonly IGameRepository _gameRepository;

    public GameAppService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GameDto> AddAsync(AddGameDto dto)
    {
        var game = new Game()
        {
            Name = dto.Name,
            Description = dto.Description,
            Genre = dto.Genre,
            Price = new Price(dto.Price),
        };

        var result = await _gameRepository.CreateAsync(game);

        return new GameDto()
        {
            Id = result.Id,
            CreationDate = result.CreationDate,
            IsActive = result.IsActive,
            Name = result.Name,
            Description = result.Description,
            Genre = result.Genre,
            Price = result.Amount
        };
    }

    public async Task<IEnumerable<GameDto>> GetAllAsync()
    {
        var games = await _gameRepository.GetAllAsync();

        return games.Select(game => new GameDto()
        {
            Id = game.Id,
            CreationDate = game.CreationDate,
            IsActive = game.IsActive,
            Name = game.Name,
            Description = game.Description,
            Genre = game.Genre,
            Price = game.Amount
        });
    }

    public async Task<GameDto> GetByIdAsync(int id)
    {
        var game = await _gameRepository.GetByIdAsync(id);

        return new GameDto()
        {
            Id = game.Id,
            CreationDate = game.CreationDate,
            IsActive = game.IsActive,
            Name = game.Name,
            Description = game.Description,
            Genre = game.Genre,
            Price = game.Amount
        };
    }

    public async Task SetActiveStatusAsync(SetActiveStatusDto dto)
    {
        var game = await _gameRepository.GetByIdAsync(dto.Id);
        game.IsActive = dto.IsActive;
        await _gameRepository.UpdateAsync(game);
    }

    public async Task UpdateAsync(UpdateGameDto dto)
    {
        var game = await _gameRepository.GetByIdAsync(dto.Id);
        
        game.Name = dto.Name;
        game.Description = dto.Description;
        game.Genre = dto.Genre;
        game.Price = new Price(dto.Price);

        await _gameRepository.UpdateAsync(game);
    }

    public async Task SetPriceAsync(SetPriceDto dto)
    {
        var game = await _gameRepository.GetByIdAsync(dto.Id);
        game.Price = new Price(dto.Price);
        await _gameRepository.UpdateAsync(game);
    }
}
