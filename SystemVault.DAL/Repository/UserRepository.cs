using Microsoft.EntityFrameworkCore;
using SystemVault.DAL.Context;
using SystemVault.DAL.Interfaces;
using SystemVault.DAL.Models;

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
}