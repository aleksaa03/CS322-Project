using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.BLL.Interfaces;
using SystemVault.Presentation.Helpers;

namespace SystemVault.Presentation.Views.Windows;

public partial class AddFileWindow : Window
{
    private readonly IServiceFileService _serviceFileService;
    private readonly ICategoryService _categoryService;

    public AddFileWindow(IServiceFileService serviceFileService, ICategoryService categoryService)
    {
        InitializeComponent();
        _serviceFileService = serviceFileService;
        _categoryService = categoryService;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string name = txbName.Text;
        string path = txbPath.Text;
        int categoryId = Convert.ToInt32(((KeyValuePair<string, string>)cmbCategoryId.SelectedItem).Value);

        string filename = Path.GetFileName(txbFilepath.Text);

        if (string.IsNullOrEmpty(name))
        {
            name = filename;
        }

        string destinationPath = Path.Combine(path, name + Path.GetExtension(filename));

        var serviceFile = new ServiceFileDto
        {
            Name = name,
            Path = destinationPath,
            CategoryId = categoryId,
            Size = Convert.ToInt64(txbSize.Text)
        };

        File.Copy(txbFilepath.Text, destinationPath, true);

        await _serviceFileService.CreateAsync(serviceFile);
        await _serviceFileService.SaveChangesAsync();

        MessageBoxHelper.ShowInfo($"{name + Path.GetExtension(filename)} added succesfully!");

        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void BrowsePathButton_Click(object sender, RoutedEventArgs e)
    {
        var openFolderDialog = new OpenFolderDialog();

        if (openFolderDialog.ShowDialog() == true)
        {
            txbPath.Text = openFolderDialog.FolderName;
        }
    }

    private void BrowseFileButton_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == true)
        {
            txbFilepath.Text = openFileDialog.FileName;

            var fileInfo = new FileInfo(openFileDialog.FileName);
            txbSize.Text = fileInfo.Length.ToString();
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