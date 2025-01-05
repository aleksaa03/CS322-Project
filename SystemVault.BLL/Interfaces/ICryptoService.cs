namespace SystemVault.BLL.Interfaces;

public interface ICryptoService
{
    void EncryptFile(string inputFilepath, string outputFilepath, string password);
    void DecryptFile(string inputFilepath, string outputFilepath, string password);
}