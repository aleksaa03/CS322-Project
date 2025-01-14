using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.DTOs;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Common;
using SystemVault.Presentation.Helpers;

namespace SystemVault.Presentation.Views.Windows.User;

public partial class AddUserWindow : Window
{
    private readonly IUserService _userService;
    private readonly ICryptoService _cryptoService;

    public event EventHandler? OnSubmit;

    public AddUserWindow(IUserService userService, ICryptoService cryptoService)
    {
        InitializeComponent();
        _userService = userService;
        _cryptoService = cryptoService;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string username = txbUsername.Text;
        string password = pwbPassword.Password;
        var roleId = (UserRole?)Convert.ToInt32(((KeyValuePair<string, string>)cmbUserRole.SelectedItem).Value);

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || roleId == null)
        {
            return;
        }

        var user = new UserDto
        {
            Username = username,
            Password = _cryptoService.HashPassword(password),
            RoleId = (UserRole)roleId
        };

        await _userService.CreateAsync(user);
        await _userService.SaveChangesAsync();
        OnSubmit?.Invoke(this, EventArgs.Empty);
        Close();

        MessageBoxHelper.ShowInfo($"{username} added succesfully!");
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void UserRoleComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        var comboBox = sender as ComboBox;

        var list = Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(x => new KeyValuePair<string, string>(x.ToString(), ((int)x).ToString()));

        comboBox?.Items.Clear();

        foreach (var item in list)
        {
            comboBox?.Items.Add(item);
        }
    }
}