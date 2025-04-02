using EntityFrameWorkTp.Data;
using EntityFrameWorkTp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkTp.Infrastructure.Repositories;

public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T:class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public ReadOnlyRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
}