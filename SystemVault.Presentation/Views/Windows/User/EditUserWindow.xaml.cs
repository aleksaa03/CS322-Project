using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.Common;
using SystemVault.BLL.DTOs;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Common;
using SystemVault.Presentation.Helpers;

namespace SystemVault.Presentation.Views.Windows.User;

public partial class EditUserWindow : Window
{
    private readonly IUserService _userService;

    public UserDto? User = null;

    public event EventHandler? OnSubmit;

    public EditUserWindow(IUserService userService)
    {
        InitializeComponent();
        _userService = userService;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DataContext = User;

        if (User == null)
            throw new Exception("User is empty");
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string username = txbUsername.Text;

        if (string.IsNullOrEmpty(username))
        {
            throw new Exception("Name is not defined.");
        }

        if (cmbUserRole.SelectedItem == null)
        {
            throw new Exception("User role is not defined.");
        }

        var roleId = (UserRole?)Convert.ToInt32(((KeyValuePair<string, string>)cmbUserRole.SelectedItem).Value);

        var selectedUser = await _userService.GetByIdAsync(User!.Id);

        if (selectedUser == null) return;

        selectedUser.Username = username;
        selectedUser.RoleId = (UserRole)roleId;

        _userService.Update(selectedUser);
        await _userService.SaveChangesAsync();

        OnSubmit?.Invoke(this, EventArgs.Empty);
        Close();

        MessageBoxHelper.ShowInfo($"{username} updated succesfully!");
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
            .Select(x => new KeyValuePair<string, string>(x.ToString(), ((int)x).ToString()))
            .ToList();

        comboBox.ItemsSource = list;
        comboBox.SelectedIndex = list.FindIndex(x => x.Key == User?.RoleId.ToString());

        if (User?.Id == UserSession.CurrentUser?.Id)
        {
            comboBox.IsEnabled = false;
        }
    }
}