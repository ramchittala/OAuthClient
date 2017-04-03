using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OauthClient
{
    /// <summary>
    /// Mobile OAuth Client
    /// </summary>
    public class OAuthClient
    {
        /// <summary>
        /// The _HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The _address
        /// </summary>
        private readonly Uri _address;

        /// <summary>
        /// The _client identifier
        /// </summary>
        private readonly string _clientId;

        /// <summary>
        /// The _client secret
        /// </summary>
        private readonly string _clientSecret;

        /// <summary>
        /// The offline scope
        /// </summary>
        private readonly string _scope;

        /// <summary>
        /// The values
        /// </summary>
        private Dictionary<string, string> values;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileOAuthClient"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecretId">The client secret identifier.</param>
        public OAuthClient(string uri, string clientId, string clientSecretId) : this(new Uri(uri), clientId, clientSecretId, new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileOAuthClient"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecretId">The client secret identifier.</param>
        public OAuthClient(Uri uri, string clientId, string clientSecretId) : this(uri, clientId, clientSecretId, new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileOAuthClient"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecretId">The client secret identifier.</param>
        /// <param name="scope">The scope.</param>
        public OAuthClient(string uri, string clientId, string clientSecretId, string scope) : this(new Uri(uri), clientId, clientSecretId, scope, new HttpClientHandler())
        {
        }

        #region Configuration with Scope
        /// <summary>
        /// Initializes a new instance of the <see cref="MobileOAuthClient"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecretId">The client secret identifier.</param>
        /// <param name="scope">The scope.</param>
        public OAuthClient(Uri uri, string clientId, string clientSecretId, string scope) : this(uri, clientId, clientSecretId, scope, new HttpClientHandler())
        {

        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="MobileOAuthClient"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecretId">The client secret identifier.</param>
        /// <param name="clientHandler">The client handler.</param>
        /// <exception cref="System.ArgumentNullException">clientHandler</exception>
        public OAuthClient(Uri uri, string clientId, string clientSecretId, HttpClientHandler clientHandler)
        {
            if (clientHandler == null)
            {
                throw new ArgumentNullException("clientHandler");
            }

            _httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = uri
            };

            _address = uri;
            _clientId = clientId;
            _clientSecret = clientSecretId;
            values = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(_clientId))
                values.Add(OAuthConstants.ClientId, _clientId);
            if (!string.IsNullOrWhiteSpace(_clientSecret))
                values.Add(OAuthConstants.ClientSecret, _clientSecret);
        }

        #region ctor with scope included
        /// <summary>
        /// Initializes a new instance of the <see cref="MobileOAuthClient"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecretId">The client secret identifier.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="clientHandler">The client handler.</param>
        /// <exception cref="System.ArgumentNullException">clientHandler</exception>
        public OAuthClient(Uri uri, string clientId, string clientSecretId, string scope, HttpClientHandler clientHandler)
        {
            if (clientHandler == null)
            {
                throw new ArgumentNullException("clientHandler");
            }

            _httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = uri
            };

            _address = uri;
            _clientId = clientId;
            _clientSecret = clientSecretId;
            _scope = scope;
            values = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(_clientId))
                values.Add(OAuthConstants.ClientId, _clientId);
            if (!string.IsNullOrWhiteSpace(_clientSecret))
                values.Add(OAuthConstants.ClientSecret, _clientSecret);
            if (!string.IsNullOrWhiteSpace(_scope))
                values.Add(OAuthConstants.Scope, _scope);
        }
        #endregion

        /// <summary>
        /// Requests the acces token asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// userName
        /// or
        /// password
        /// </exception>
        public async Task<TokenResponse> RequestAccesTokenAsync(string userName, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            values = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(_clientId))
                values.Add(OAuthConstants.ClientId, _clientId);
            if (!string.IsNullOrWhiteSpace(_clientSecret))
                values.Add(OAuthConstants.ClientSecret, _clientSecret);
            if (!string.IsNullOrWhiteSpace(_scope))
                values.Add(OAuthConstants.Scope, _scope);
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(OAuthConstants.UserName);
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(OAuthConstants.UserName);
            values.Add(OAuthConstants.GrantType, OAuthConstants.GrantTypes.Password);
            values.Add(OAuthConstants.UserName, userName);
            values.Add(OAuthConstants.Password, password);
            return await RequestAsync(values, cancellationToken);
        }


        /// <summary>
        /// Requests the refresh token asynchronous.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">refreshToken</exception>
        public async Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentNullException("refreshToken");

            values = GetRefreshTokenValues(refreshToken);
            return await RequestAsync(values, cancellationToken);
        }

        public async Task<TokenResponse> RequestTokenWithClientCredentialsAsync(string grantType, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(grantType))
                throw new ArgumentNullException("grantType should not be empty");

            values = GetClientCredsValues(grantType);
            return await RequestAsync(values, cancellationToken);
        }

        /// <summary>
        /// Requests the asynchronous.
        /// </summary>
        /// <param name="formValues">The form values.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TokenResponse> RequestAsync(Dictionary<string, string> formValues, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _httpClient.PostAsync(string.Empty, new FormUrlEncodedContent(formValues), cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return new TokenResponse(content);
            }
            else
            {
                return new TokenResponse(response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Gets the refresh token values.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        private Dictionary<string, string> GetRefreshTokenValues(string refreshToken)
        {
            var values = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(_clientId))
                values.Add(OAuthConstants.ClientId, _clientId);
            if (!string.IsNullOrWhiteSpace(_clientSecret))
                values.Add(OAuthConstants.ClientSecret, _clientSecret);
            if (!string.IsNullOrWhiteSpace(_scope)) //added scope in config
                values.Add(OAuthConstants.Scope, _scope);

            values.Add(OAuthConstants.GrantType, OAuthConstants.GrantTypes.RefreshToken);

            values.Add(OAuthConstants.GrantTypes.RefreshToken, refreshToken);

            return values;
        }

        // <summary>
        /// <summary>
        /// Gets the client creds values.
        /// </summary>
        /// <param name="grantType">Type of the grant.</param>
        /// <returns></returns>
        private Dictionary<string, string> GetClientCredsValues(string grantType)
        {
            var values = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(_clientId))
                values.Add(OAuthConstants.ClientId, _clientId);
            if (!string.IsNullOrWhiteSpace(_clientSecret))
                values.Add(OAuthConstants.ClientSecret, _clientSecret);
            if (!string.IsNullOrWhiteSpace(_scope)) //added scope in config
                values.Add(OAuthConstants.Scope, _scope);

            values.Add(OAuthConstants.GrantType, grantType);

            return values;
        }

    }
}
