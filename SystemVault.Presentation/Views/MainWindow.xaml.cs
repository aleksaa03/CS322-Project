using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using SystemVault.Presentation.Views.UserControls;

namespace SystemVault.Presentation.Views;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private void FileMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var fileView = _serviceProvider.GetRequiredService<FileView>();
        ChangeView(fileView);
    }

    private void CategoriesMenuItem_Click(object sender, RoutedEventArgs e)
    {

    }

    private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ChangeView(UserControl uc)
    {
        MainContent.Children.Clear();
        MainContent.Children.Add(uc);
    }
}