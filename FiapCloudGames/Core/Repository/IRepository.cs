using Core.Entity.Base;

namespace Core.Repository;
public interface IRepository<T> where T : EntityBase
{
    IList<T> GetAll();
    T GetById(int id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
