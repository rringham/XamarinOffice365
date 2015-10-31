using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Xamarin.Forms;
using XamarinOffice365.Interfaces;

[assembly: Dependency(typeof(XamarinOffice365.iOS.Authentication.IosAzureActiveDirectoryAuthenticator))]

namespace XamarinOffice365.iOS.Authentication
{
    public class IosAzureActiveDirectoryAuthenticator : IAzureActiveDirectoryAuthenticator
    {
        public async Task<string> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(controller);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);

            var accessToken = authResult.AccessToken;
            return accessToken;
        }
    }
}