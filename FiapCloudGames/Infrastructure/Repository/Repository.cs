using Core.Entity.Base;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected ApplicationDbContext _context;
    protected DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        entity.CreationDate = DateTime.Now;
        entity.IsActive = true;
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IList<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<T> GetByIdAsync(int id)
        => await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id) ?? throw new KeyNotFoundException($"Id {id} não encontrado.");

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
