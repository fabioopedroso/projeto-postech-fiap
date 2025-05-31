using Application.DTOs.Game.Result;
using Application.DTOs.Game.Signature;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Core.Entity;
using Core.Interfaces.Repository;

namespace Application.Services;
public class GameAppService : IGameAppService, IGameReadOnlyAppService
{
    private readonly IGameRepository _gameRepository;
    private readonly IGameCacheService _gameCacheService;

    public GameAppService(IGameRepository gameRepository, IGameCacheService gameCacheService)
    {
        _gameRepository = gameRepository;
        _gameCacheService = gameCacheService;
    }

    public async Task<GameDto> AddAsync(AddGameDto dto)
    {
        var game = new Game()
        {
            Name = dto.Name,
            Description = dto.Description,
            Genre = dto.Genre,
            Price = dto.Price,
        };

        var result = await _gameRepository.CreateAsync(game);
        _gameCacheService.InvalidateGamesCache();

        return new GameDto()
        {
            Id = result.Id,
            CreationDate = result.CreationDate,
            IsActive = result.IsActive,
            Name = result.Name,
            Description = result.Description,
            Genre = result.Genre,
            Price = result.Price
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
            Price = game.Price
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
            Price = game.Price
        };
    }

    public async Task SetActiveStatusAsync(SetActiveStatusDto dto)
    {
        var game = await _gameRepository.GetByIdAsync(dto.Id);
        game.IsActive = dto.IsActive;
        await _gameRepository.UpdateAsync(game);

        _gameCacheService.InvalidateGamesCache();
        _gameCacheService.InvalidateGameCache(dto.Id);
    }

    public async Task UpdateAsync(UpdateGameDto dto)
    {
        var game = await _gameRepository.GetByIdAsync(dto.Id);
        
        game.Name = dto.Name;
        game.Description = dto.Description;
        game.Genre = dto.Genre;
        game.Price = dto.Price;

        await _gameRepository.UpdateAsync(game);

        _gameCacheService.InvalidateGamesCache();
        _gameCacheService.InvalidateGameCache(dto.Id);
    }

    public async Task SetPriceAsync(SetPriceDto dto)
    {
        var game = await _gameRepository.GetByIdAsync(dto.Id);
        game.Price = dto.Price;
        await _gameRepository.UpdateAsync(game);

        _gameCacheService.InvalidateGamesCache();
        _gameCacheService.InvalidateGameCache(dto.Id);
    }
}
