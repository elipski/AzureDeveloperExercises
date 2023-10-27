using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace az204_auth 
{
    class Program{

    private const string _clientId = "3c9d940c-5405-4310-b4c6-973b0f2bcf5a";
    private const string _tenantId = "f7aa6e8f-547c-4b9b-abc2-7dd1ec4ce92b";
        public static async Task Main(string[] args){
            var app = PublicClientApplicationBuilder
                    .Create(_clientId)
                    .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                    .WithRedirectUri("http://localhost")
                    .Build();
            string[] scopes = { "user.read" };
            AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

            Console.WriteLine($"Token:\t{result.AccessToken}");

        }
    }
}

