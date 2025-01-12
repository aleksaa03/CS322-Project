using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.Events;
using SystemVault.BLL.Interfaces;
using SystemVault.BLL.Services;
using SystemVault.Presentation.Views.UserControls;

namespace SystemVault.Presentation.Views;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRegistryService _registryService;

    public MainWindow(IServiceProvider serviceProvider, IRegistryService registryService)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _registryService = registryService;

        UserSessonChangedEvent.UserSessionChanged += (s, e) =>
        {
            if (e.IsUserLoggedIn)
            {
                mnuMain.Visibility = Visibility.Visible;

                var fileView = _serviceProvider.GetRequiredService<FileView>();
                ChangeView(fileView);
            }
            else
            {
                UserSession.CurrentUser = null;
                mnuMain.Visibility = Visibility.Collapsed;

                Dispatcher.Invoke(() =>
                {
                    var loginView = _serviceProvider.GetRequiredService<LoginView>();
                    ChangeView(loginView);
                });
            }
        };

        UserSessonChangedEvent.OnUserSessionChanged(false);
    }

    private string GenerateKey()
    {
        string key = Guid.NewGuid().ToString().Replace("-", "");
        return key;
        /*int length = guid.Length;

        string firstHalf = guid.Substring(0, length / 2);
        string secondHalf = guid.Substring(length / 2);

        string test = "";

        for (int i = 1; i < guid.Length; i += 2)
        {
            test += guid[i].ToString() + guid[i - 1].ToString();
        }

        return secondHalf + firstHalf;*/
    }

    private void FileMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (!UserSession.IsUserLoggedIn())
        {
            UserSessonChangedEvent.OnUserSessionChanged(false);
            return;
        }

        var fileView = _serviceProvider.GetRequiredService<FileView>();
        ChangeView(fileView);
    }

    private void CategoriesMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (!UserSession.IsUserLoggedIn())
        {
            UserSessonChangedEvent.OnUserSessionChanged(false);
            return;
        }

        var view = _serviceProvider.GetRequiredService<CategoryView>();
        ChangeView(view);
    }

    private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<UserView>();
        ChangeView(view);
    }

    private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (!UserSession.IsUserLoggedIn())
        {
            return;
        }

        UserSessonChangedEvent.OnUserSessionChanged(false);
    }

    private void ChangeView(UserControl uc)
    {
        MainContent.Children.Clear();
        MainContent.Children.Add(uc);
    }
}