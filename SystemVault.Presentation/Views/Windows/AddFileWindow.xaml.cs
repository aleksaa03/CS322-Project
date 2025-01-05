using Microsoft.Win32;
using System.IO;
using System.Windows;
using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.BLL.Interfaces;

namespace SystemVault.Presentation.Views.Windows;

public partial class AddFileWindow : Window
{
    private readonly IServiceFileService _serviceFileService;

    public AddFileWindow(IServiceFileService serviceFileService)
    {
        InitializeComponent();
        _serviceFileService = serviceFileService;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string name = txbName.Text;
        string path = txbPath.Text;
        int categoryId = Convert.ToInt32(txbCategoryId.Text);

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

        MessageBox.Show($"{name + Path.GetExtension(filename)} added succesfully!");

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
}