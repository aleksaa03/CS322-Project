using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.Common;
using SystemVault.BLL.DTOs;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Common;
using SystemVault.DAL.Models.SearchCriteria;
using SystemVault.Presentation.Helpers;
using SystemVault.Presentation.Views.Windows.User;

namespace SystemVault.Presentation.Views.UserControls;

public partial class UserView : UserControl
{
    private readonly IServiceProvider _serviceProvider;

    private readonly IUserService _userService;

    private UserSC _sc;

    public UserView(IUserService userService, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _userService = userService;
        _serviceProvider = serviceProvider;
        _sc = new UserSC();

        cmbPageSize.SelectedIndex = 0;
        cmbPageSize.SelectionChanged += PageSizeComboBox_SelectionChanged;
        cmbUserRole.Loaded += UserRoleComboBox_Loaded;

        Search(_sc);
    }

    private void Search(UserSC sc)
    {
        var list = _userService.Filter(sc);
        dtg.ItemsSource = list.ToList();
        txtRange.Text = $"Page: {sc.Page} / {sc.PageCount}";
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        _sc.Username = txbUsername.Text;
        _sc.RoleId = cmbUserRole.SelectedItem != null ? (UserRole)Convert.ToInt32(((KeyValuePair<string, string>)cmbUserRole.SelectedItem).Value) : null;
        Search(_sc);
    }

    private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
    {
        txbUsername.Text = null;
        cmbUserRole.Text = null;

        _sc = new UserSC();
        Search(_sc);
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
        var window = _serviceProvider.GetRequiredService<AddUserWindow>();
        window.OnSubmit += (s, e) =>
        {
            Search(_sc);
        };
        window.ShowDialog();
    }

    private void EditUserButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectedItem = button?.CommandParameter as UserDto;

        var window = _serviceProvider.GetRequiredService<EditUserWindow>();
        window.User = selectedItem;
        window.OnSubmit += (s, e) =>
        {
            Search(_sc);
        };

        window.ShowDialog();
    }

    private void ResetUserPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectedItem = button?.CommandParameter as UserDto;

        var window = _serviceProvider.GetRequiredService<ResetPasswordWindow>();
        window.User = selectedItem;

        window.ShowDialog();
    }

    private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectedItem = button?.CommandParameter as UserDto;

        if (selectedItem == null) return;

        if (selectedItem.Id == UserSession.CurrentUser?.Id)
        {
            throw new Exception("Can't delete current user.");
        }

        var result = MessageBoxHelper.ShowYesNo("Are you sure?");
        if (result != MessageBoxResult.Yes) return;

        await _userService.DeleteAsync(selectedItem.Id);
        await _userService.SaveChangesAsync();

        Search(_sc);

        MessageBoxHelper.ShowInfo($"{selectedItem.Username} succesfully deleted!");
    }

    private void PrevButton_Click(object sender, RoutedEventArgs e)
    {
        if (_sc.Page > 1)
        {
            _sc.Page--;
        }

        Search(_sc);
    }

    private void NextButton_Click(object sender, RoutedEventArgs e)
    {
        if (_sc.Page < _sc.PageCount)
        {
            _sc.Page++;
        }

        Search(_sc);
    }

    private void PageSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        var selectedItem = comboBox?.SelectedItem as ComboBoxItem;

        if (selectedItem != null)
        {
            _sc.PageSize = Convert.ToInt32(selectedItem.Tag);
            _sc.Page = 1;
            Search(_sc);
        }
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