using SystemVault.DAL.Context;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;

namespace SystemVault.DAL.Repository;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(SystemVaultDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Category> GetAll()
    {
        return _dbSet.AsQueryable();
    }
}