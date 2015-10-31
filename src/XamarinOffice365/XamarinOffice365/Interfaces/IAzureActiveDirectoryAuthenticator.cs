using System;
using System.Threading.Tasks;

namespace XamarinOffice365.Interfaces
{
    public interface IAzureActiveDirectoryAuthenticator
    {
        Task<string> Authenticate(string authority, string resource, string clientId, string returnUri);
    }
}