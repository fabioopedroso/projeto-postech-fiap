using Core.Entity.Base;

namespace Core.Interfaces.Repository;
public interface IRepository<T> where T : EntityBase
{
    Task<int> CreateAsync(T entity);
    IList<T> GetAll();
    T GetById(int id);
    void Update(T entity);
    void Delete(T entity);
}
