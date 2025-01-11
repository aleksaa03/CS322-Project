using System.Windows;
using SystemVault.BLL.DTOs.Category;
using SystemVault.BLL.Interfaces;
using SystemVault.Presentation.Helpers;

namespace SystemVault.Presentation.Views.Windows.Category;

public partial class AddCategoryWindow : Window
{
    private readonly ICategoryService _categoryService;

    public CategoryDto? Category = null;

    public event EventHandler? OnSubmit;

    public AddCategoryWindow(ICategoryService categoryService)
    {
        InitializeComponent();

        _categoryService = categoryService;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DataContext = Category;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string name = txbName.Text;

        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        if (Category != null && Category.Id > 0)
        {
            await Edit(Category.Id, name);
            return;
        }

        await Create(name);
    }

    private async Task Create(string name)
    {
        var category = new CategoryDto
        {
            Name = name
        };

        await _categoryService.CreateAsync(category);
        await _categoryService.SaveChangesAsync();

        OnSubmit?.Invoke(this, EventArgs.Empty);
        Close();

        MessageBoxHelper.ShowInfo($"{name} added succesfully!");
    }

    private async Task Edit(long id, string name)
    {
        var selectedCategory = await _categoryService.GetByIdAsync(id);

        if (selectedCategory == null) return;

        selectedCategory.Name = name;
        _categoryService.Update(selectedCategory);
        await _categoryService.SaveChangesAsync();

        OnSubmit?.Invoke(this, EventArgs.Empty);
        Close();

        MessageBoxHelper.ShowInfo($"{name} updated succesfully!");
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}