using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.DTOs.Category;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Models.SearchCriteria;
using SystemVault.Presentation.Helpers;
using SystemVault.Presentation.Views.Windows.Category;

namespace SystemVault.Presentation.Views.UserControls;

public partial class CategoryView : UserControl
{
    private readonly IServiceProvider _serviceProvider;

    private readonly ICategoryService _categoryService;
    private CategorySC _sc;

    public CategoryView(IServiceProvider serviceProvider, ICategoryService categoryService)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;

        _categoryService = categoryService;
        _sc = new CategorySC();

        cmbPageSize.SelectedIndex = 0;
        cmbPageSize.SelectionChanged += PageSizeComboBox_SelectionChanged;

        Search(_sc);
    }

    private void Search(CategorySC sc)
    {
        var list = _categoryService.Filter(sc);
        dtg.ItemsSource = list.ToList();
        txtRange.Text = $"Page: {sc.Page} / {sc.PageCount}";
    }

    private void PrevButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (_sc.Page > 1)
        {
            _sc.Page--;
        }

        Search(_sc);
    }

    private void NextButton_Click(object sender, System.Windows.RoutedEventArgs e)
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

    private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
    {
        var window = _serviceProvider.GetRequiredService<AddCategoryWindow>();
        window.OnSubmit += (s, e) =>
        {
            Search(_sc);
        };
        window.ShowDialog();
    }

    private void EditCategoryButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectedItem = button?.CommandParameter as CategoryDto;

        var window = _serviceProvider.GetRequiredService<AddCategoryWindow>();
        window.Category = selectedItem;
        window.OnSubmit += (s, e) =>
        {
            Search(_sc);
        };

        window.ShowDialog();
    }

    private async void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectedItem = button?.CommandParameter as CategoryDto;

        if (selectedItem == null) return;

        var result = MessageBoxHelper.ShowYesNo("Are you sure?");

        if (result != MessageBoxResult.Yes) return;

        await _categoryService.DeleteAsync(selectedItem.Id);
        await _categoryService.SaveChangesAsync();

        Search(_sc);

        MessageBoxHelper.ShowInfo($"{selectedItem.Name} succesfully deleted!");
    }

    private void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _sc.Name = txbName.Text;
        Search(_sc);
    }

    private void ResetFiltersButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        txbName.Text = null;

        _sc = new CategorySC();
        Search(_sc);
    }
}