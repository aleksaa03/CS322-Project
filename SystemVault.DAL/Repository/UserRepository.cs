using Microsoft.EntityFrameworkCore;
using SystemVault.DAL.Context;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.DAL.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(SystemVaultDbContext dbContext) : base(dbContext)
    {
    }   

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Username == username);
    }

    public IQueryable<User> Filter(UserSC sc)
    {
        var list = _dbSet.OrderByDescending(x => x.Id).AsQueryable();

        if (!string.IsNullOrEmpty(sc.Username))
        {
            list = list.Where(x => x.Username.Contains(sc.Username));
        }

        if (sc.RoleId != null) 
        {
            list = list.Where(x => x.RoleId == sc.RoleId);    
        }

        sc.PageCount = (int)Math.Ceiling((decimal)list.Count() / sc.PageSize);

        list = list.Skip((sc.Page - 1) * sc.PageSize).Take(sc.PageSize);

        return list;
    }
}