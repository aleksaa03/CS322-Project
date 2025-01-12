using SystemVault.DAL.Models;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.DAL.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    IQueryable<User> Filter(UserSC sc);
}