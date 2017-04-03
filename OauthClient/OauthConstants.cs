﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OauthClient
{
    public class OAuthConstants
    {
        public const string GrantType = "grant_type";
        public const string UserName = "username";
        public const string Scope = "scope";
        public const string Assertion = "assertion";
        public const string Password = "password";
        public const string RedirectUri = "redirect_uri";
        public const string AccessToken = "access_token";
        public const string ExpiresIn = "expires_in";
        public const string TokenType = "token_type";
        public const string RefreshToken = "refresh_token";
        public const string IdentityToken = "id_token";
        public const string ClientId = "client_id";
        public const string ClientSecret = "client_secret";
        public const string ResponseType = "response_type";
        public const string LoginHint = "login_hint";
        public const string Error = "error";
        public const string ResponseMode = "response_mode";
        public const string Google = "Google";
        public const string ProviderName = "provider_name";
        public const string Email = "email";

        public static class GrantTypes
        {
            public const string Password = "password";
            public const string AuthorizationCode = "authorization_code";
            public const string ClientCredentials = "client_credentials";
            public const string RefreshToken = "refresh_token";
            public const string KekaMobileApp = "kekamobileapp";
        }

        public static class ResponseTypes
        {
            public const string Token = "token";
            public const string Code = "code";
        }

        public static class Errors
        {
            public const string Error = "error";
            public const string InvalidRequest = "invalid_request";
            public const string InvalidClient = "invalid_client";
            public const string InvalidGrant = "invalid_grant";
            public const string UnauthorizedClient = "unauthorized_client";
            public const string UnsupportedGrantType = "unsupported_grant_type";
            public const string UnsupportedResponseType = "unsupported_response_type";
            public const string InvalidScope = "invalid_scope";
            public const string AccessDenied = "access_denied";
        }
    }
}
