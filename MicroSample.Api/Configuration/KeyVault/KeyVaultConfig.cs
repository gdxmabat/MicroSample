using Koa.Configuration.Secrets.Azure.KeyVault;
using Microsoft.Extensions.Configuration;

namespace MicroSample.Api.Configuration.KeyVault
{
    internal static class KeyVaultConfig
    {
        public static IConfigurationBuilder ConfigureEnvironment(IConfigurationBuilder builder)
        {
            var config = builder.Build();

            var keyVaultConfig = config.GetSection(KeyVaultConfiguration.Section).Get<KeyVaultConfiguration>();
            if (keyVaultConfig?.IsEnabled == true)
            {
                builder.AddAzureKeyVault(keyVaultConfig.Uri, keyVaultConfig.ClientId, keyVaultConfig.ClientSecret,
                    new PrefixedKeyVaultsecrectManager(keyVaultConfig.ApiPrefix));
               
            }

            return builder;
        }
    }
}