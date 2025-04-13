using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.Events;
using SystemVault.BLL.Interfaces;
using SystemVault.Presentation.Views.UserControls;
using SystemVault.DAL.Common;
using SystemVault.BLL.Common;
using SystemVault.BLL.DTOs;

namespace SystemVault.Presentation.Views;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRegistryService _registryService;
    private readonly IUserService _userService;
    private readonly ICryptoService _cryptoService;

    public MainWindow(IServiceProvider serviceProvider, IRegistryService registryService, IUserService userService, ICryptoService cryptoService)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _registryService = registryService;
        _userService = userService;
        _cryptoService = cryptoService;

        //var admin = new UserDto
        //{
        //    Username = "admin",
        //    Password = _cryptoService.HashPassword("test123"),
        //    RoleId = UserRole.Admin
        //};

        //var user = new UserDto
        //{
        //    Username = "client",
        //    Password = _cryptoService.HashPassword("test321"),
        //    RoleId = UserRole.Operator
        //};

        //Task.Run(async () =>
        //{
        //    await _userService.CreateAsync(admin);
        //    await _userService.CreateAsync(user);
        //    await _userService.SaveChangesAsync();
        //});

        UserSessonChangedEvent.UserSessionChanged += (s, e) =>
        {
            if (e.IsUserLoggedIn)
            {
                mnuMain.Visibility = Visibility.Visible;

                if (UserSession.CurrentUser?.RoleId != UserRole.Admin)
                {
                    mniUsers.Visibility = Visibility.Collapsed;
                }
                else
                {
                    mniUsers.Visibility = Visibility.Visible;
                }

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