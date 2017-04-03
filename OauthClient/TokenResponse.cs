using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OauthClient
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Gets or sets the raw.
        /// </summary>
        /// <value>
        /// The raw.
        /// </value>
        public string Raw { get; protected set; }

        /// <summary>
        /// Gets or sets the json.
        /// </summary>
        /// <value>
        /// The json.
        /// </value>
        public JObject Json { get; protected set; }

        /// <summary>
        /// The _is HTTP error
        /// </summary>
        private bool _isHttpError;

        /// <summary>
        /// The _HTTP errorstatus code
        /// </summary>
        private HttpStatusCode _httpErrorstatusCode;

        /// <summary>
        /// The _HTTP error reason
        /// </summary>
        private string _httpErrorReason;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenResponse"/> class.
        /// </summary>
        /// <param name="raw">The raw.</param>
        public TokenResponse(string raw)
        {
            Raw = raw;
            Json = JObject.Parse(raw);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="reason">The reason.</param>
        public TokenResponse(HttpStatusCode statusCode, string reason)
        {
            _isHttpError = true;
            _httpErrorstatusCode = statusCode;
            _httpErrorReason = reason;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is HTTP error.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is HTTP error; otherwise, <c>false</c>.
        /// </value>
        public bool IsHttpError
        {
            get
            {
                return _isHttpError;
            }
        }

        /// <summary>
        /// Gets the HTTP error status code.
        /// </summary>
        /// <value>
        /// The HTTP error status code.
        /// </value>
        public HttpStatusCode HttpErrorStatusCode
        {
            get
            {
                return _httpErrorstatusCode;
            }
        }

        /// <summary>
        /// Gets the HTTP error reason.
        /// </summary>
        /// <value>
        /// The HTTP error reason.
        /// </value>
        public string HttpErrorReason
        {
            get
            {
                return _httpErrorReason;
            }
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken
        {
            get
            {
                return GetStringOrNull(OAuthConstants.AccessToken);
            }
        }

        /// <summary>
        /// Gets the identity token.
        /// </summary>
        /// <value>
        /// The identity token.
        /// </value>
        public string IdentityToken
        {
            get
            {
                return GetStringOrNull(OAuthConstants.IdentityToken);
            }
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error
        {
            get
            {
                return GetStringOrNull(OAuthConstants.Error);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError
        {
            get
            {
                return (IsHttpError ||
                        !string.IsNullOrWhiteSpace(GetStringOrNull(OAuthConstants.Error)));
            }
        }

        /// <summary>
        /// Gets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        public long ExpiresIn
        {
            get
            {
                return GetLongOrNull(OAuthConstants.ExpiresIn);
            }
        }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public string TokenType
        {
            get
            {
                return GetStringOrNull(OAuthConstants.TokenType);
            }
        }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        public string RefreshToken
        {
            get
            {
                return GetStringOrNull(OAuthConstants.RefreshToken);
            }
        }

        /// <summary>
        /// Gets the string or null.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected virtual string GetStringOrNull(string name)
        {
            JToken value;
            if (Json != null && Json.TryGetValue(name, StringComparison.OrdinalIgnoreCase, out value))
            {
                return value.ToString();
            }

            return null;
        }

        /// <summary>
        /// Gets the long or null.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected virtual long GetLongOrNull(string name)
        {
            JToken value;
            if (Json != null && Json.TryGetValue(name, out value))
            {
                long longValue = 0;
                if (long.TryParse(value.ToString(), out longValue))
                {
                    return longValue;
                }
            }
            return 0;
        }
    }
}
