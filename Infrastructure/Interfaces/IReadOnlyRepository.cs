namespace EntityFrameWorkTp.Infrastructure.Interfaces;

public interface IReadOnlyRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
}