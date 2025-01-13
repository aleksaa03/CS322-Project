using SystemVault.DAL.Common;

namespace SystemVault.BLL.DTOs;

public class UserDto : BaseDto
{
    public required string Username { get; set; }
    public UserRole RoleId { get; set; }
}