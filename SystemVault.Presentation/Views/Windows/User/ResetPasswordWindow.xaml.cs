using System.Windows;
using SystemVault.BLL.DTOs;
using SystemVault.BLL.Interfaces;
using SystemVault.Presentation.Helpers;

namespace SystemVault.Presentation.Views.Windows.User;

public partial class ResetPasswordWindow : Window
{
    private IUserService _userService;
    private ICryptoService _cryptoService;

    public UserDto? User = null;

    public ResetPasswordWindow(IUserService userService, ICryptoService cryptoService)
    {
        InitializeComponent();
        _userService = userService;
        _cryptoService = cryptoService;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DataContext = User;

        if (User == null)
            throw new Exception("User is empty");
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string password = pwbPassword.Password;

        if (string.IsNullOrEmpty(password))
        {
            return;
        }

        var selectedUser = await _userService.GetByIdAsync(User!.Id);

        if (selectedUser == null) return;

        selectedUser.Password = _cryptoService.HashPassword(password);

        _userService.Update(selectedUser);
        await _userService.SaveChangesAsync();

        Close();

        MessageBoxHelper.ShowInfo($"{selectedUser.Username} password updated succesfully!");
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void GenerateRandomPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890-_=+[{}]:;,<.>/?";
        string randomPassword = string.Empty;

        var random = new Random();

        for (int i = 0; i < 10; i++)
        {
            randomPassword += chars[random.Next(0, chars.Length)];
        }

        txbRandomPassword.Text = randomPassword;
    }
}