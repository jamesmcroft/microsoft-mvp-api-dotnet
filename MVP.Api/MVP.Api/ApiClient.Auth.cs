namespace MVP.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models.MicrosoftAccount;

    using WinUX;

    public partial class ApiClient
    {
        private const string RedirectUri = "https://login.live.com/oauth20_desktop.srf";

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
            return this.RetrieveAuthenticationUri(scopes.Select(x => x.GetDescriptionAttribute()), redirectUri);
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
            var scopesStr = string.Join(" ", scopes);

            return
                $"{AuthenticationUri}?redirect_uri={Uri.EscapeUriString(redirectUri ?? RedirectUri)}&client_id={Uri.EscapeUriString(this.ClientId)}&scope={Uri.EscapeUriString(scopesStr)}&response_type=code";
        }

        public Task<MSACredentials> ExchangeCodeAsync(
            string code,
            bool isTokenRefresh = false,
            CancellationTokenSource cts = null)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code), "The code for exchange cannot be null or empty.");
            }

            return Task.FromResult(default(MSACredentials));
        }

        /// <summary>
        /// Logs out the user from the Microsoft services asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a value indicating whether the logout was successful.
        /// </returns>
        public async Task<bool> LogOutAsync()
        {
            var logoutSuccess = false;
            try
            {
                var uri =
                    $"{LogOutUri}?redirect_uri={Uri.EscapeUriString(RedirectUri)}&client_id={Uri.EscapeUriString(this.ClientId)}";

                var client = new HttpClient();
                var response = await client.GetAsync(uri);
                logoutSuccess = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }
            finally
            {
                this.Credentials = null;
            }

            return logoutSuccess;
        }
    }
}