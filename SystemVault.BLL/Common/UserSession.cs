using SystemVault.BLL.DTOs;

namespace SystemVault.BLL.Common;

public static class UserSession
{
    public static UserDto? CurrentUser { get; set; }

    public static bool IsUserLoggedIn()
    {
        return CurrentUser != null;
    }
}