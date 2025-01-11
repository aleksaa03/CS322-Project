using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SystemVault.BLL.Events;
using SystemVault.BLL.Interfaces;
using SystemVault.BLL.Services;

namespace SystemVault.Presentation.Views.UserControls;

public partial class LoginView : UserControl
{
    private readonly IUserService _userService;

    public LoginView(IUserService userService)
    {
        InitializeComponent();

        _userService = userService;
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        await Login();
    }

    private async void OnInputEnter_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            await Login();
        }
    }

    private async Task Login()
    {
        string username = txbUsername.Text;
        string password = pwbPassword.Password;

        var user = await _userService.LoginUserAsync(username, password);

        if (user == null)
        {
            return;
        }

        UserSession.CurrentUser = user;
        UserSessonChangedEvent.OnUserSessionChanged(true);
    }
}