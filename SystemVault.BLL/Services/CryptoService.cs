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

        using var memoryStream = new MemoryStream();

        memoryStream.Write(aes.IV, 0, aes.IV.Length);

        using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

        using var fsInput = new FileStream(inputFilepath, FileMode.Open);
        fsInput.CopyTo(cryptoStream);
        cryptoStream.FlushFinalBlock();

        var encryptedData = Convert.ToBase64String(memoryStream.ToArray());

        File.WriteAllText(outputFilepath, encryptedData);
    }

    public void DecryptFile(string inputFilepath, string outputFilepath, string password)
    {
        using var aes = Aes.Create();

        aes.Key = SHA256.HashData(Encoding.ASCII.GetBytes(password));

        string base64Content = File.ReadAllText(inputFilepath);
        byte[] encryptedData = Convert.FromBase64String(base64Content);

        using var memoryStream = new MemoryStream(encryptedData);

        byte[] iv = new byte[aes.IV.Length];
        memoryStream.Read(iv, 0, iv.Length);

        aes.IV = iv;

        using var fsOutput = new FileStream(outputFilepath, FileMode.Create);
        using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);

        cryptoStream.CopyTo(fsOutput);
    }

    public string HashPassword(string password)
    {
        string saltedPassword = password + GenerateSalt();

        byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
        byte[] hash = SHA256.HashData(bytes);

        var result = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("x2"));
        }

        return result.ToString();
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return hashedPassword == HashPassword(password);
    }

    private string GenerateSalt()
    {
        var rng = RandomNumberGenerator.Create();
        byte[] buffer = new byte[1024];

        rng.GetBytes(buffer);

        string salt = BitConverter.ToString(buffer);

        return salt;
    }
}