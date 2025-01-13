using SystemVault.DAL.Common;

namespace SystemVault.DAL.Models.SearchCriteria;

public class UserSC : SCBase
{
    public string? Username { get; set; }
    public UserRole? RoleId { get; set; }
}