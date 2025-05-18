using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ILibraryRepository Libraries { get; }
        ICartRepository Carts { get; }
        IGameRepository Games { get; }
        ISaleRepository Sales { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
