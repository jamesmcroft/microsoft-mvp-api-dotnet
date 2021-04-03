namespace MVP.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models.MicrosoftAccount;
    using MVP.Api.Networking.Requests;

    public partial class ApiClient
    {
        public const string RedirectUri = "https://login.live.com/oauth20_desktop.srf";

        private const string AuthenticationUri = "https://login.live.com/oauth20_authorize.srf";

        private const string LogOutUri = "https://login.live.com/oauth20_logout.srf";

        private const string TokenUri = "https://login.live.com/oauth20_token.srf";

        /// <summary>
        /// Constructs a URL for authentication with Microsoft services and MVP API.
        /// </summary>
        /// <param name="scopes">
        /// A list of scopes to access during authentication.
        /// </param>
        /// <param name="redirectUri">
        /// An optional redirect URI.
        /// </param>
        /// <returns>
        /// Returns the constructed authentication URI.
        /// </returns>
        public string RetrieveAuthenticationUri(IEnumerable<MSAScope> scopes, string redirectUri = null)
        {
            return this.RetrieveAuthenticationUri(
                scopes.Select(x => MSAScopeNameAttribute.GetScopeName(x)),
                redirectUri);
        }

        /// <summary>
        /// Constructs a URL for authentication with Microsoft services and MVP API.
        /// </summary>
        /// <param name="scopes">
        /// A list of scopes as a string to access during authentication.
        /// </param>
        /// <param name="redirectUri">
        /// An optional redirect URI.
        /// </param>
        /// <returns>
        /// Returns the constructed authentication URI.
        /// </returns>
        public string RetrieveAuthenticationUri(IEnumerable<string> scopes, string redirectUri = null)
        {
            string scopesStr = string.Join(" ", scopes);

            return
                $"{AuthenticationUri}?redirect_uri={Uri.EscapeUriString(redirectUri ?? RedirectUri)}&client_id={Uri.EscapeUriString(this.ClientId)}&scope={Uri.EscapeUriString(scopesStr)}&response_type=code";
        }

        /// <summary>
        /// Exchanges the stored refresh token for the authenticated user for a new access token asynchronously.
        /// </summary>
        /// <param name="cts">
        /// A cancellation token source.
        /// </param>
        /// <returns>
        /// Returns the Microsoft credentials for the user with an access token.
        /// </returns>
        public async Task<MSACredentials> ExchangeRefreshTokenAsync(CancellationTokenSource cts = null)
        {
            var msaCredentials = this.Credentials;
            if (msaCredentials == null || string.IsNullOrWhiteSpace(msaCredentials.RefreshToken))
            {
                throw new ApiException(ApiExceptionCode.AuthenticationError, "No credentials or refresh token exist.");
            }

            MSACredentials response = await this.ExchangeAuthCodeAsync(this.Credentials.RefreshToken, true, cts);
            this.Credentials = response;

            return response;
        }

        /// <summary>
        /// Exchanges the authentication code or refresh token for an access token from Microsoft services.
        /// </summary>
        /// <param name="code">
        /// The auth code or refresh token to exchange.
        /// </param>
        /// <param name="isTokenRefresh">
        /// A value indicating whether the code is a refresh token.
        /// </param>
        /// <param name="cts">
        /// A cancellation token source.
        /// </param>
        /// <returns>
        /// Returns the Microsoft credentials for the user with an access token.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the code for exchanging is null or empty.
        /// </exception>
        public async Task<MSACredentials> ExchangeAuthCodeAsync(
            string code,
            bool isTokenRefresh = false,
            CancellationTokenSource cts = null)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ApiException(
                    ApiExceptionCode.AuthenticationError,
                    "The auth code for exchange cannot be null or empty.");
            }

            MSACredentials response;

            if (this.IsLiveSdkApp)
            {
                string uri = isTokenRefresh
                                 ? $"{TokenUri}?redirect_uri={Uri.EscapeUriString(RedirectUri)}&client_id={Uri.EscapeUriString(this.ClientId)}&client_secret={Uri.EscapeUriString(this.ClientSecret)}&refresh_token={Uri.EscapeUriString(code)}&grant_type=refresh_token"
                                 : $"{TokenUri}?redirect_uri={Uri.EscapeUriString(RedirectUri)}&client_id={Uri.EscapeUriString(this.ClientId)}&client_secret={Uri.EscapeUriString(this.ClientSecret)}&code={Uri.EscapeUriString(code)}&grant_type=authorization_code";

                response = await this.GetAsync<MSACredentials>(string.Empty, false, uri, cts);
            }
            else
            {
                List<KeyValuePair<string, string>> data =
                    new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>(
                                "client_id",
                                this.ClientId),
                            new KeyValuePair<string, string>(
                                "redirect_uri",
                                RedirectUri),
                            new KeyValuePair<string, string>(
                                "grant_type",
                                isTokenRefresh
                                    ? "refresh_token"
                                    : "authorization_code"),
                            new KeyValuePair<string, string>(
                                isTokenRefresh ? "refresh_token" : "code",
                                code)
                        };

                FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(data);

                response = await this.PostAuthCodeAsync(encodedContent, cts);
            }

            this.Credentials = response;

            return response;
        }

        private async Task<MSACredentials> PostAuthCodeAsync(
            FormUrlEncodedContent data,
            CancellationTokenSource cts = null)
        {
            FormUrlEncodedJsonPostNetworkRequest postRequest =
                new FormUrlEncodedJsonPostNetworkRequest(this.httpClient, TokenUri, data);

            bool retryCall = false;

            try
            {
                return await postRequest.ExecuteAsync<MSACredentials>(cts);
            }
            catch (HttpRequestException hre) when (hre.Message.Contains("401"))
            {
                MSACredentials tokenRefreshed = await this.ExchangeRefreshTokenAsync();
                if (tokenRefreshed != null)
                {
                    retryCall = true;
                }
            }

            if (!retryCall)
            {
                return default(MSACredentials);
            }

            postRequest = new FormUrlEncodedJsonPostNetworkRequest(this.httpClient,TokenUri, data);
            return await postRequest.ExecuteAsync<MSACredentials>(cts);
        }

        /// <summary>
        /// Logs out the user from the Microsoft services asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a value indicating whether the logout was successful.
        /// </returns>
        public async Task<bool> LogOutAsync()
        {
            bool logoutSuccess = false;
            try
            {
                string uri =
                    $"{LogOutUri}?redirect_uri={Uri.EscapeUriString(RedirectUri)}&client_id={Uri.EscapeUriString(this.ClientId)}";

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(uri);
                logoutSuccess = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            finally
            {
                this.Credentials = null;
            }

            return logoutSuccess;
        }
    }
}