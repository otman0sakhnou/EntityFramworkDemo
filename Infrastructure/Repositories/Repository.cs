using EntityFrameWorkTp.Data;
using EntityFrameWorkTp.Infrastructure.Interfaces;
using EntityFrameWorkTp.utils;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkTp.Infrastructure.Repositories;

public class Repository<T>  : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }


    
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    
    public void Update(T entity) => _dbSet.Update(entity);
    
    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new RepositoryException("A concurrency error occurred while saving changes", ex);
        }
        catch (DbUpdateException ex)
        {
            throw new RepositoryException("An error occurred while saving changes to the database", ex);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("An unexpected error occurred", ex);
        }
    }
}