using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using XamarinOffice365.Interfaces;

[assembly: Dependency(typeof(XamarinOffice365.Droid.Authentication.DroidAzureActiveDirectoryAuthenticator))]

namespace XamarinOffice365.Droid.Authentication
{
    public class DroidAzureActiveDirectoryAuthenticator : IAzureActiveDirectoryAuthenticator
    {
        public async Task<string> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters((Activity)Forms.Context);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);

            var accessToken = authResult.AccessToken;
            return accessToken;
        }
    }
}