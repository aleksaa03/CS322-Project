namespace SystemVault.BLL.Events;

public static class UserSessonChangedEvent
{
    public static event EventHandler<UserSessionChangedEventsArgs>? UserSessionChanged;

    public static void OnUserSessionChanged(bool isUserLoggedIn)
    {
        UserSessionChanged?.Invoke(null, new UserSessionChangedEventsArgs(isUserLoggedIn));
    } 
}

public class UserSessionChangedEventsArgs : EventArgs
{
    public bool IsUserLoggedIn { get; set; }

    public UserSessionChangedEventsArgs(bool isUserLoggedIn)
    {
        IsUserLoggedIn = isUserLoggedIn;
    }
}