using System.Security.Cryptography;
using System.Text;
using SystemVault.BLL.Interfaces;

namespace SystemVault.BLL.Services;

public class CryptoService : ICryptoService
{
    public void EncryptFile(string inputFilepath, string outputFilepath, string password)
    {
        using var aes = Aes.Create();
        aes.GenerateIV();

        aes.Key = SHA256.HashData(Encoding.ASCII.GetBytes(password));

        using var fsOutput = new FileStream(outputFilepath, FileMode.Create);
        using var cryptoStream = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write);

        fsOutput.Write(aes.IV, 0, aes.IV.Length);

        using var fsInput = new FileStream(inputFilepath, FileMode.Open);

        fsInput.CopyTo(cryptoStream);
    }

    public void DecryptFile(string inputFilepath, string outputFilepath, string password)
    {
        using var aes = Aes.Create();

        aes.Key = SHA256.HashData(Encoding.ASCII.GetBytes(password));

        using var fsInput = new FileStream(inputFilepath, FileMode.Open);
        using var fsOutput = new FileStream(outputFilepath, FileMode.Create);

        byte[] iv = new byte[aes.IV.Length];
        fsInput.Read(iv, 0, iv.Length);

        aes.IV = iv;

        using var cryptoStream = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read);

        cryptoStream.CopyTo(fsOutput);
    }
}