using Application.DTOs.Library.Result;

namespace Application.Interfaces;

public interface ILibraryAppService
{
    Task<IEnumerable<LibraryGamesDto>> ListLibraryGamesAsync();
}
