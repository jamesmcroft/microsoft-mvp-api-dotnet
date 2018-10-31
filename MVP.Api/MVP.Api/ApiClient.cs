namespace MVP.Api
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using MADE.App.Networking.Requests.Json;

    using MVP.Api.Models.MicrosoftAccount;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines a mechanism to call into the MVP API from a client application.
    /// </summary>
    public partial class ApiClient
    {
        private const string BaseApiUri = "https://mvpapi.azure-api.net/mvp/api";

        private readonly HttpClient httpClient;

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
        /// <param name="isLiveSdkApp">
        /// A value indicating whether the client ID and secret are associated with an older Live SDK application.
        /// </param>
        public ApiClient(string clientId, string clientSecret, string subscriptionKey, bool isLiveSdkApp = false)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.SubscriptionKey = subscriptionKey;
            this.IsLiveSdkApp = isLiveSdkApp;

            this.httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets a value indicating whether the client ID and secret are from an older Live SDK application.
        /// </summary>
        /// <remarks>
        /// If you're using a newer 'Converged application', this should be false.
        /// If you're using an older 'Live SDK application', this should be true.
        /// Find your apps here: https://apps.dev.microsoft.com/?mkt=en-us#/appList
        /// </remarks>
        public bool IsLiveSdkApp { get; }

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
        /// Gets or sets the credentials associated with the logged in Microsoft account.
        /// </summary>
        public MSACredentials Credentials { get; set; }

        private async Task<TResponse> GetAsync<TResponse>(
            string endpoint,
            bool useCredentials = true,
            string overrideUri = null,
            CancellationTokenSource cts = null)
        {
            string uri = string.IsNullOrWhiteSpace(overrideUri) ? $"{BaseApiUri}/{endpoint}" : overrideUri;

            JsonGetNetworkRequest getRequest = useCredentials
                                                   ? new JsonGetNetworkRequest(
                                                       this.httpClient,
                                                       uri,
                                                       this.GetRequestHeaders())
                                                   : new JsonGetNetworkRequest(this.httpClient, uri);

            bool retryCall = false;

            try
            {
                return await getRequest.ExecuteAsync<TResponse>(cts);
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
                return default(TResponse);
            }

            getRequest = useCredentials
                             ? new JsonGetNetworkRequest(this.httpClient, uri, this.GetRequestHeaders())
                             : new JsonGetNetworkRequest(this.httpClient, uri);
            return await getRequest.ExecuteAsync<TResponse>(cts);
        }

        private async Task<TResponse> PostAsync<TResponse>(
            string endpoint,
            object data,
            bool useCredentials = true,
            string overrideUri = null,
            CancellationTokenSource cts = null)
        {
            string uri = string.IsNullOrWhiteSpace(overrideUri) ? $"{BaseApiUri}/{endpoint}" : overrideUri;

            JsonPostNetworkRequest postRequest = useCredentials
                                                     ? new JsonPostNetworkRequest(
                                                         this.httpClient,
                                                         uri,
                                                         JsonConvert.SerializeObject(data),
                                                         this.GetRequestHeaders())
                                                     : new JsonPostNetworkRequest(
                                                         this.httpClient,
                                                         uri,
                                                         JsonConvert.SerializeObject(data));

            bool retryCall = false;

            try
            {
                return await postRequest.ExecuteAsync<TResponse>(cts);
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
                return default(TResponse);
            }

            postRequest = useCredentials
                              ? new JsonPostNetworkRequest(
                                  this.httpClient,
                                  uri,
                                  JsonConvert.SerializeObject(data),
                                  this.GetRequestHeaders())
                              : new JsonPostNetworkRequest(this.httpClient, uri, JsonConvert.SerializeObject(data));

            return await postRequest.ExecuteAsync<TResponse>(cts);
        }

        private async Task<bool> PutAsync(
            string endpoint,
            object data,
            bool useCredentials = true,
            string overrideUri = null,
            CancellationTokenSource cts = null)
        {
            string uri = string.IsNullOrWhiteSpace(overrideUri) ? $"{BaseApiUri}/{endpoint}" : overrideUri;

            JsonPutNetworkRequest putRequest = useCredentials
                                                   ? new JsonPutNetworkRequest(
                                                       this.httpClient,
                                                       uri,
                                                       JsonConvert.SerializeObject(data),
                                                       this.GetRequestHeaders())
                                                   : new JsonPutNetworkRequest(
                                                       this.httpClient,
                                                       uri,
                                                       JsonConvert.SerializeObject(data));

            bool retryCall = false;

            try
            {
                await putRequest.ExecuteAsync<string>(cts);
                return true;
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
                return false;
            }

            putRequest = useCredentials
                             ? new JsonPutNetworkRequest(
                                 this.httpClient,
                                 uri,
                                 JsonConvert.SerializeObject(data),
                                 this.GetRequestHeaders())
                             : new JsonPutNetworkRequest(this.httpClient, uri, JsonConvert.SerializeObject(data));
            await putRequest.ExecuteAsync<string>(cts);
            return true;
        }

        private async Task<bool> DeleteAsync(
            string endpoint,
            bool useCredentials = true,
            string overrideUri = null,
            CancellationTokenSource cts = null)
        {
            string uri = string.IsNullOrWhiteSpace(overrideUri) ? $"{BaseApiUri}/{endpoint}" : overrideUri;

            JsonDeleteNetworkRequest deleteRequest = useCredentials
                                                         ? new JsonDeleteNetworkRequest(
                                                             this.httpClient,
                                                             uri,
                                                             this.GetRequestHeaders())
                                                         : new JsonDeleteNetworkRequest(this.httpClient, uri);

            bool retryCall = false;

            try
            {
                await deleteRequest.ExecuteAsync<string>(cts);
                return true;
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
                return false;
            }

            deleteRequest = useCredentials
                                ? new JsonDeleteNetworkRequest(this.httpClient, uri, this.GetRequestHeaders())
                                : new JsonDeleteNetworkRequest(this.httpClient, uri);
            await deleteRequest.ExecuteAsync<string>(cts);
            return true;

        }

        private Dictionary<string, string> GetRequestHeaders()
        {
            if (this.Credentials == null || string.IsNullOrWhiteSpace(this.Credentials.AccessToken))
            {
                return new Dictionary<string, string>();
            }

            Dictionary<string, string> headers = new Dictionary<string, string>
                                                     {
                                                         { "Authorization", $"Bearer {this.Credentials.AccessToken}" },
                                                         { "Ocp-Apim-Subscription-Key", this.SubscriptionKey }
                                                     };

            return headers;
        }
    }
}