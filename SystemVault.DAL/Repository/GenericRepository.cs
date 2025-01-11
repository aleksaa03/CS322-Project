using Microsoft.EntityFrameworkCore;
using SystemVault.DAL.Context;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;

namespace SystemVault.DAL.Repository;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : Base
{
    public SystemVaultDbContext _dbContext;
    public DbSet<T> _dbSet;

    public GenericRepository(SystemVaultDbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    public void Update(T entity)
    {
        var trackedEntity = _dbContext.ChangeTracker.Entries<T>()
                                      .FirstOrDefault(e => e.Entity.Id == entity.Id);

        if (trackedEntity != null)
        {
            trackedEntity.State = EntityState.Detached;
        }

        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task<bool> EntityExistAsync(long id)
    {
        return await _dbSet.AsNoTracking().AnyAsync(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}