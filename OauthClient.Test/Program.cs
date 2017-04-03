using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OauthClient.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "Https://yourtoken-endpoit-url";
            string clientId = "ssdkjsweyiweru";
            string clientsecret = "sfgsjdgfsd";
            string scope = "offline_access";
            string username = "test";
            string password = "sdfsd";
            string refreshtoken = "sfsdfksdhfkjshkdjfskjhdfkjshdfk";
            string grantType = "dasdasd"; //custome or client credentials

            OAuthClient client = new OAuthClient(uri, clientId, clientsecret, scope); //Resource Based Configuration with Scope

            //Get Token by client id, client secret, username, password
            var tokenResponse1 = client.RequestAccesTokenAsync(username, password).Result;

            //Get Token By refresh token
            var tokenResponse2 = client.RequestRefreshTokenAsync(refreshtoken).Result;

            //Get Token By client Creds
            var tokenResponse3 = client.RequestTokenWithClientCredentialsAsync(grantType).Result;

            //Get token by custom form data values
            var tokenResponse4 = client.RequestAsync(new Dictionary<string, string>());
        }
    }
}
