namespace MVP.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models.MicrosoftAccount;

    using WinUX;
    using WinUX.Networking.Requests.Json;

    /// <summary>
    /// Defines a mechanism to call into the MVP API from a client application.
    /// </summary>
    public partial class ApiClient
    {
        private const string RedirectUri = "https://login.live.com/oauth20_desktop.srf";

        private const string BaseApiUri = "https://mvpapi.azure-api.net/mvp";

        private const string AuthenticationUri = "https://login.live.com/oauth20_authorize.srf";

        private const string LogOutUri = "https://login.live.com/oauth20_logout.srf";

        private const string TokenUri = "https://login.live.com/oauth20_token.srf";

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="clientId">
        /// The Microsoft application client ID.
        /// </param>
        /// <param name="clientSecret">
        /// The Microsoft application client secret.
        /// </param>
        /// <param name="subscriptionKey">
        /// The MVP API subscription key.
        /// </param>
        public ApiClient(string clientId, string clientSecret, string subscriptionKey)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.SubscriptionKey = subscriptionKey;
        }

        /// <summary>
        /// Gets the Microsoft application client ID.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Gets the Microsoft application client secret.
        /// </summary>
        public string ClientSecret { get; }

        /// <summary>
        /// Gets the MVP API subscription key.
        /// </summary>
        public string SubscriptionKey { get; }


        /// <summary>
        /// Gets the credentials associated with the logged in Microsoft account.
        /// </summary>
        public MSACredentials Credentials { get; private set; }

        /// <summary>
        /// Constructs a URL for authentication with Microsoft services and MVP API.
        /// </summary>
        /// <param name="scopes">
        /// A list of scopes to access during authentication.
        /// </param>
        /// <param name="redirectUrl">
        /// An optional redirect URI.
        /// </param>
        /// <returns>
        /// Returns the constructed authentication URI.
        /// </returns>
        public string RetrieveAuthenticationUri(IEnumerable<MSAScope> scopes, string redirectUrl = null)
        {
            return this.RetrieveAuthenticationUri(scopes.Select(x => x.GetDescriptionAttribute()), redirectUrl);
        }

        /// <summary>
        /// Constructs a URL for authentication with Microsoft services and MVP API.
        /// </summary>
        /// <param name="scopes">
        /// A list of scopes as a string to access during authentication.
        /// </param>
        /// <param name="redirectUrl">
        /// An optional redirect URI.
        /// </param>
        /// <returns>
        /// Returns the constructed authentication URI.
        /// </returns>
        public string RetrieveAuthenticationUri(IEnumerable<string> scopes, string redirectUrl = null)
        {
            var uri = new UriBuilder(AuthenticationUri);
            var query = new StringBuilder();

            query.AppendFormat("redirect_uri={0}", Uri.EscapeUriString(redirectUrl ?? RedirectUri));
            query.AppendFormat("&client_id={0}", Uri.EscapeUriString(this.ClientId));

            var scopesStr = string.Join(" ", scopes);
            query.AppendFormat("&scope={0}", Uri.EscapeUriString(scopesStr));
            query.Append("&response_type=code");

            uri.Query = query.ToString();

            return uri.Uri.ToString();
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

        private Dictionary<string, string> GetRequestHeaders()
        {
            if (!string.IsNullOrWhiteSpace(this.Credentials?.AccessToken))
            {
                var headers = new Dictionary<string, string>
                                  {
                                      {
                                          "Authorization",
                                          $"Bearer {this.Credentials.AccessToken}"
                                      }
                                  };
                return headers;
            }

            return new Dictionary<string, string>();
        }
    }
}