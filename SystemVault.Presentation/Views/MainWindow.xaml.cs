using System.Windows;
using System.Windows.Controls;
using SystemVault.Presentation.Views.UserControls;

namespace SystemVault.Presentation.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void FileMenuItem_Click(object sender, RoutedEventArgs e)
    {
        ChangeView(new FileView());
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