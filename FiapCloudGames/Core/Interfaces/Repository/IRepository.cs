using Core.Entity.Base;

namespace Core.Interfaces.Repository;
public interface IRepository<T> where T : EntityBase
{
    Task<T> CreateAsync(T entity);
    Task<IList<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
