namespace EntityFrameWorkTp.Infrastructure.Interfaces;

public interface IRepository<T> where T :  class
{
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveChangesAsync();
}