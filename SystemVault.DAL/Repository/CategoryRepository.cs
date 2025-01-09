using SystemVault.DAL.Context;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

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

    public IQueryable<Category> Filter(CategorySC sc)
    {
        var list = _dbSet.AsQueryable();

        if (!string.IsNullOrEmpty(sc.Name))
        {
            list = list.Where(x => x.Name.Contains(sc.Name));
        }

        sc.PageCount = (int)Math.Ceiling((decimal)list.Count() / sc.PageSize);

        list = list.Skip((sc.Page - 1) * sc.PageSize).Take(sc.PageSize);

        return list;
    }
}