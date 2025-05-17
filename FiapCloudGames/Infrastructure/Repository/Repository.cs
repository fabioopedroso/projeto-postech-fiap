using Core.Entity.Base;
using Core.Repository;
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

    public void Create(T entity)
    {
        entity.CreationDate = DateTime.Now;
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public IList<T> GetAll()
        => _dbSet.ToList();

    public T GetById(int id)
        => _dbSet.FirstOrDefault(entity => entity.Id == id);

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
}
