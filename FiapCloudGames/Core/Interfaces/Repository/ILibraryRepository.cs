using Core.Entity;

namespace Core.Interfaces.Repository
{
    public interface ILibraryRepository
    {
        Task CreateAsync(Library library);
    }
}