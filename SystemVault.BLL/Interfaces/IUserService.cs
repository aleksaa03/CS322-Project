using SystemVault.BLL.DTOs;

namespace SystemVault.BLL.Interfaces;

public interface IUserService : IGenericService<UserDto>
{
    Task<UserDto?> GetByUsernameAsync(string username);
    Task<UserDto?> LoginUserAsync(string username, string password);
}