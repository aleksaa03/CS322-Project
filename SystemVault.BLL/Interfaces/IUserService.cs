using SystemVault.BLL.DTOs;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.BLL.Interfaces;

public interface IUserService : IGenericService<UserDto>
{
    Task<UserDto?> GetByUsernameAsync(string username);
    Task<UserDto?> LoginUserAsync(string username, string password);
    IQueryable<UserDto> Filter(UserSC sc);
}