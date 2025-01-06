using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Models.SearchCriteria;
using SystemVault.Presentation.Helpers;
using SystemVault.Presentation.Views.Windows;

namespace SystemVault.Presentation.Views.UserControls;

public partial class FileView : UserControl
{
    private readonly IServiceProvider _serviceProvider;

    private readonly IServiceFileService _serviceFileService;
    private readonly ICategoryService _categoryService;
    private ServiceFileSC _serviceFileSC;

    public FileView(IServiceProvider serviceProvider, IServiceFileService serviceFileService, ICategoryService categoryService)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;

        _serviceFileService = serviceFileService;
        _categoryService = categoryService;
        _serviceFileSC = new ServiceFileSC();

        cmbPageSize.SelectedIndex = 0;
        cmbPageSize.SelectionChanged += PageSizeComboBox_SelectionChanged;
        cmbCategoryId.Loaded += CategoryComboBox_Loaded;

        Search(_serviceFileSC);
    }

    private void Search(ServiceFileSC sc)
    {
        var list = _serviceFileService.Filter(sc);
        dtg.ItemsSource = list.ToList();
        txtRange.Text = $"Page: {sc.Page} / {sc.PageCount}";
    }

    private void PrevButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (_serviceFileSC.Page > 1)
        {
            _serviceFileSC.Page--;
        }

        Search(_serviceFileSC);
    }

    private void NextButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (_serviceFileSC.Page < _serviceFileSC.PageCount)
        {
            _serviceFileSC.Page++;
        }

        Search(_serviceFileSC);
    }

    private void PageSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        var selectedItem = comboBox?.SelectedItem as ComboBoxItem;

        if (selectedItem != null)
        {
            _serviceFileSC.PageSize = Convert.ToInt32(selectedItem.Tag);
            _serviceFileSC.Page = 1;
            Search(_serviceFileSC);
        }
    }

    private void AddServiceFileButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var addFileWindow = _serviceProvider.GetRequiredService<AddFileWindow>();
        addFileWindow.ShowDialog();
    }

    private void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _serviceFileSC.Name = txbName.Text;
        Search(_serviceFileSC);
    }

    private void ResetFiltersButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        txbName.Text = null;
        cmbCategoryId.Text = null;

        _serviceFileSC = new ServiceFileSC();
        Search(_serviceFileSC);
    }

    private void FileEdit_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectedFile = button?.CommandParameter as ServiceFileDto;

        var addFileWindow = _serviceProvider.GetRequiredService<AddFileWindow>();
        addFileWindow.DataContext = selectedFile;

        addFileWindow.ShowDialog();
    }

    private async void FileDelete_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var button = sender as Button;
        var selectedFile = button?.CommandParameter as ServiceFileDto;

        if (selectedFile == null) return;

        await _serviceFileService.DeleteAsync(selectedFile.Id);
        await _serviceFileService.SaveChangesAsync();

        Search(_serviceFileSC);

        MessageBoxHelper.ShowInfo($"{selectedFile.Name} succesfully deleted!");
    }

    private void CategoryComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        var comboBox = sender as ComboBox;

        var categories = _categoryService.GetAll();

        comboBox?.Items.Clear();

        foreach (var item in categories)
        {
            comboBox?.Items.Add(new KeyValuePair<string, string>(item.Name, item.Id.ToString()));
        }
    }
}