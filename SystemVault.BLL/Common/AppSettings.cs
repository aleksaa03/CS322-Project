using System.Runtime.Versioning;
using SystemVault.BLL.Services;

namespace SystemVault.BLL.Common;

[SupportedOSPlatform("windows")]
public static class AppSettings
{
    public static string EncryptionKey 
    { 
        get
        {
            string? key = RegistryService.ReadStatic("Key");

            if (string.IsNullOrEmpty(key)) 
            {
                string generatedKey = Guid.NewGuid().ToString().Replace("-", "");
                RegistryService.WriteStatic("Key", generatedKey);

                return generatedKey;
            }

            return key;
        } 
    }
}