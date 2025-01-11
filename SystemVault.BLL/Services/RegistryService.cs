using Microsoft.Win32;
using System.Runtime.Versioning;
using SystemVault.BLL.Interfaces;

namespace SystemVault.BLL.Services;

[SupportedOSPlatform("windows")]
public class RegistryService : IRegistryService
{
    private const string REGISTRY_PATH = "Software\\SystemVault";

    public static void WriteStatic(string key, string value)
    {
        string registryPath = REGISTRY_PATH;
        using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(registryPath);

        registryKey?.SetValue(key, value);
    }

    public static string? ReadStatic(string key)
    {
        using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(REGISTRY_PATH);

        if (registryKey == null)
        {
            return null;
        }

        object? value = registryKey.GetValue(key);

        return value?.ToString();
    }

    public void Write(string key, string value)
    {
        string registryPath = REGISTRY_PATH;
        using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(registryPath);

        registryKey?.SetValue(key, value);
    }

    public string? Read(string key)
    {
        using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(REGISTRY_PATH);

        if (registryKey == null)
        {
            return null;
        }

        object? value = registryKey.GetValue(key);

        return value?.ToString();
    }
}