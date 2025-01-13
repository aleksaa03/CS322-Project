using SystemVault.DAL.Context;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.DAL.Repository;

public class ServiceFileRepository : GenericRepository<ServiceFile>, IServiceFileRepository
{
    public ServiceFileRepository(SystemVaultDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<ServiceFile> Filter(ServiceFileSC sc)
    {
        var list = _dbSet.OrderByDescending(x => x.CreatedAt).AsQueryable();

        if (!string.IsNullOrEmpty(sc.Name))
        {
            list = list.Where(x => x.Name.Contains(sc.Name));
        }

        if (sc.CategoryId != null)
        {
            list = list.Where(x => x.CategoryId == sc.CategoryId);
        }

        sc.PageCount = (int)Math.Ceiling((decimal)list.Count() / sc.PageSize);

        list = list.Skip((sc.Page - 1) * sc.PageSize).Take(sc.PageSize);

        return list;
    }
}