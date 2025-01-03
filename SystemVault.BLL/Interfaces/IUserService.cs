using SystemVault.BLL.DTOs;

namespace SystemVault.BLL.Interfaces;

public interface IUserService : IGenericService<UserDto>
{
    Task<UserDto?> GetByUsernameAsync(string username);
}