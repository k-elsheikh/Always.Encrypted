using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Always.Encrypted.Web.Models
{
    public partial class Always_Encrypted_DBContext : DbContext
    {
        static bool IsInitialized;
        readonly IConfiguration _IConfiguration;
        static ClientCredential _ClientCredential;
        public Always_Encrypted_DBContext(DbContextOptions<Always_Encrypted_DBContext> options, IConfiguration IConfiguration)
    : base(options)
        {
            _IConfiguration = IConfiguration;
            if (!IsInitialized)
            { InitializeAzureKeyVaultProvider(); IsInitialized = true; }

            void InitializeAzureKeyVaultProvider()
            {
                _ClientCredential = new ClientCredential(_IConfiguration["AzureAd:ClientId"], _IConfiguration["AzureAd:ClientSecret"]);

                SqlColumnEncryptionAzureKeyVaultProvider azureKeyVaultProvider =
                  new SqlColumnEncryptionAzureKeyVaultProvider(GetToken);

                Dictionary<string, SqlColumnEncryptionKeyStoreProvider> providers =
                  new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>();

                providers.Add(SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, azureKeyVaultProvider);
                SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);
            }

            async Task<string> GetToken(string authority, string resource, string scope)
            {
                var authContext = new AuthenticationContext(authority);
                AuthenticationResult result = await authContext.AcquireTokenAsync(resource, _ClientCredential);

                if (result == null)
                    throw new InvalidOperationException("Failed to obtain the access token");
                return result.AccessToken;
            }
        }
    }
}
