﻿using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Models.SearchCriteria;

namespace SystemVault.Presentation.Views.UserControls;

public partial class UserView : UserControl
{
    private readonly IUserService _userService;

    private UserSC _sc;

    public UserView(IUserService userService)
    {
        InitializeComponent();

        _userService = userService;
        _sc = new UserSC();

        cmbPageSize.SelectedIndex = 0;
        cmbPageSize.SelectionChanged += PageSizeComboBox_SelectionChanged;

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
        Search(_sc);
    }

    private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
    {
        txbUsername.Text = null;

        _sc = new UserSC();
        Search(_sc);
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void EditUserButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
    {

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
}