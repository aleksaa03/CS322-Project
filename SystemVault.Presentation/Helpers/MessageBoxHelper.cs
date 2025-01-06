using System.Windows;

namespace SystemVault.Presentation.Helpers;

public static class MessageBoxHelper
{
    private static readonly string _infoCaption = "Info";
    private static readonly string _warningCaption = "Warning";
    private static readonly string _errorCaption = "Error";

    public static void ShowInfo(string message, string? caption = null)
    {
        if (string.IsNullOrEmpty(caption))
        {
            caption = _infoCaption;
        }

        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void ShowWarning(string message, string? caption = null)
    {
        if (string.IsNullOrEmpty(caption))
        {
            caption = _warningCaption;
        }

        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public static void ShowError(string message, string? caption = null)
    {
        if (string.IsNullOrEmpty(caption))
        {
            caption = _errorCaption;
        }

        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}