using Application.Contracts;
using Application.DTOs.Library.Result;
using Application.Interfaces;
using Core.Interfaces.Repository;

namespace Application.Services;
public class LibraryAppService : ILibraryAppService
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly ICurrentUseService _currentUser;

    public LibraryAppService(ILibraryRepository libraryRepository, ICurrentUseService currentUser)
    {
        _libraryRepository = libraryRepository;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<LibraryGamesDto>> ListLibraryGamesAsync()
    {
        var games = await _libraryRepository.GetGamesByUserIdAsync(_currentUser.UserId);
        
        return games.Select(game => new LibraryGamesDto
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Genre = game.Genre
        });
    }
}
