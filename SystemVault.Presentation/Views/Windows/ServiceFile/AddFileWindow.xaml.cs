﻿using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using SystemVault.BLL.Common;
using SystemVault.BLL.DTOs.ServiceFile;
using SystemVault.BLL.Interfaces;
using SystemVault.Presentation.Helpers;

namespace SystemVault.Presentation.Views.Windows.ServiceFile;

public partial class AddFileWindow : Window
{
    private readonly IServiceFileService _serviceFileService;
    private readonly ICategoryService _categoryService;
    private readonly ICryptoService _cryptoService;

    public event EventHandler? OnSubmit;

    public AddFileWindow(IServiceFileService serviceFileService, ICategoryService categoryService, ICryptoService cryptoService)
    {
        InitializeComponent();
        _serviceFileService = serviceFileService;
        _categoryService = categoryService;
        _cryptoService = cryptoService;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string name = txbName.Text;
        string dirDestinationPath = txbDestinationPath.Text;

        if (string.IsNullOrEmpty(dirDestinationPath))
        {
            throw new Exception("Vault location is not defined.");
        }

        if (string.IsNullOrEmpty(name)) 
        {
            throw new Exception("Name is not defined.");
        }

        if (cmbCategoryId.SelectedItem == null)
        {
            throw new Exception("Category is not defined.");
        }
        
        int categoryId = Convert.ToInt32(((KeyValuePair<string, string>)cmbCategoryId.SelectedItem).Value);

        string sourceFilePath = txbSourceFile.Text;
        string sourceFilename = Path.GetFileName(sourceFilePath);

        if (string.IsNullOrEmpty(name))
        {
            name = sourceFilename;
        }

        string destinationPath = Path.Combine(dirDestinationPath, name + Path.GetExtension(sourceFilename));

        var serviceFile = new ServiceFileDto
        {
            Name = name,
            Path = destinationPath,
            CategoryId = categoryId,
            Size = Convert.ToInt64(txbSize.Text)
        };

        _cryptoService.EncryptFile(sourceFilePath, destinationPath, AppSettings.EncryptionKey);

        await _serviceFileService.CreateAsync(serviceFile);
        await _serviceFileService.SaveChangesAsync();

        OnSubmit?.Invoke(this, EventArgs.Empty);
        Close();

        MessageBoxHelper.ShowInfo($"{name + Path.GetExtension(sourceFilename)} added succesfully!");
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void BrowseFileButton_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == true)
        {
            txbSourceFile.Text = openFileDialog.FileName;

            var fileInfo = new FileInfo(openFileDialog.FileName);
            txbSize.Text = fileInfo.Length.ToString();
        }
    }

    private void BrowsePathButton_Click(object sender, RoutedEventArgs e)
    {
        var openFolderDialog = new OpenFolderDialog();

        if (openFolderDialog.ShowDialog() == true)
        {
            txbDestinationPath.Text = openFolderDialog.FolderName;
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