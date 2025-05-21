using Core.Interfaces.Repository;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public IUserRepository Users { get; }
        public ILibraryRepository Libraries { get; }
        public ICartRepository Carts { get; }
        public IGameRepository Games { get; }
        public ISaleRepository Sales { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IUserRepository userRepository,
            ILibraryRepository libraryRepository,
            ICartRepository cartRepository)
        {
            _context = context;
            Users = userRepository;
            Libraries = libraryRepository;
            Carts = cartRepository;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _context.SaveChangesAsync();
                    await _transaction.CommitAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
                else
                {
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
