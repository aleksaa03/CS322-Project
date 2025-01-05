using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using SystemVault.BLL.Interfaces;
using SystemVault.DAL.Models.SearchCriteria;
using SystemVault.Presentation.Views.Windows;

namespace SystemVault.Presentation.Views.UserControls;

public partial class FileView : UserControl
{
    private readonly IServiceProvider _serviceProvider;

    private readonly IServiceFileService _serviceFileService;
    private ServiceFileSC _serviceFileSC;

    public FileView(IServiceProvider serviceProvider, IServiceFileService serviceFileService)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;

        _serviceFileService = serviceFileService;
        _serviceFileSC = new ServiceFileSC();

        Search(_serviceFileSC);
    }

    private void Search(ServiceFileSC sc)
    {
        var list = _serviceFileService.Filter(sc);
        dtg.ItemsSource = list.ToList();
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
        _serviceFileSC = new ServiceFileSC();
        Search(_serviceFileSC);
    }
}