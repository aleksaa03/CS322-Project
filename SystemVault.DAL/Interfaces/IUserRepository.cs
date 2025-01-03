using SystemVault.DAL.Models;

namespace SystemVault.DAL.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}