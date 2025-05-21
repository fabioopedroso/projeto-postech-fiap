using Core.Entity;

namespace Core.Interfaces.Repository
{
    public interface ILibraryRepository
    {
        Task CreateAsync(Library library);
        Task<IEnumerable<Game>> GetGamesByUserIdAsync(int userId);
        Task AddGames(int userId, IEnumerable<Game> games);
    }
}