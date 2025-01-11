namespace SystemVault.BLL.Interfaces;

public interface IRegistryService
{
    void Write(string key, string value);
    string? Read(string key);
}