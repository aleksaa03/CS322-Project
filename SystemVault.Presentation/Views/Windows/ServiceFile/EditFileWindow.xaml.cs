using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.BLL.Interfaces;
using SystemVault.Presentation.Helpers;

namespace SystemVault.Presentation.Views.Windows.ServiceFile;

public partial class EditFileWindow : Window
{
    private readonly IServiceFileService _serviceFileService;
    private readonly ICategoryService _categoryService;

    public ServiceFileDto? ServiceFile = null;

    public event EventHandler? OnSubmit;

    public EditFileWindow(IServiceFileService serviceFileService, ICategoryService categoryService)
    {
        InitializeComponent();
        _serviceFileService = serviceFileService;
        _categoryService = categoryService;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DataContext = ServiceFile;

        if (ServiceFile == null)
            throw new Exception("Service file is empty");
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string name = txbName.Text;
        int categoryId = Convert.ToInt32(((KeyValuePair<string, string>)cmbCategoryId.SelectedItem).Value);

        var selectedFile = await _serviceFileService.GetByIdAsync(ServiceFile!.Id);

        if (selectedFile == null) return;

        if (ServiceFile?.Path != txbFilepath.Text)
        {
            File.Move(ServiceFile!.Path, txbFilepath.Text);
            selectedFile.Path = txbFilepath.Text;
        }

        selectedFile.Name = name;
        selectedFile.CategoryId = categoryId;

        _serviceFileService.Update(selectedFile);
        await _serviceFileService.SaveChangesAsync();

        OnSubmit?.Invoke(this, EventArgs.Empty);
        Close();

        MessageBoxHelper.ShowInfo($"{name + Path.GetExtension(txbFilepath.Text)} edited succesfully!");
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void BrowseFileButton_Click(object sender, RoutedEventArgs e)
    {
        var saveFileDialog = new SaveFileDialog
        {
            FileName = Path.GetFileName(txbFilepath.Text),
            DefaultExt = Path.GetExtension(txbFilepath.Text),
            Filter = $"{Path.GetExtension(txbFilepath.Text).ToUpper()} Files|*{Path.GetExtension(txbFilepath.Text)}"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            txbFilepath.Text = saveFileDialog.FileName;
        }
    }

    private void CategoryComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        var comboBox = sender as ComboBox;

        if (comboBox == null)
        {
            return;
        }

        var categories = _categoryService.GetAll();

        foreach (var item in categories)
        {
            comboBox.Items.Add(new KeyValuePair<string, string>(item.Name, item.Id.ToString()));
        }
    }
}