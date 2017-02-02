namespace MVP.Api
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models.MicrosoftAccount;

    using WinUX.Data.Serialization;
    using WinUX.Networking.Requests.Json;

    /// <summary>
    /// Defines a mechanism to call into the MVP API from a client application.
    /// </summary>
    public partial class ApiClient
    {
        private const string BaseApiUri = "https://mvpapi.azure-api.net/mvp/api";

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
        /// Gets or sets the credentials associated with the logged in Microsoft account.
        /// </summary>
        public MSACredentials Credentials { get; set; }

        private async Task<TResponse> GetAsync<TResponse>(
            string endpoint,
            bool useCredentials = true,
            string overrideUri = null,
            CancellationTokenSource cts = null)
        {
            var uri = string.IsNullOrWhiteSpace(overrideUri) ? $"{BaseApiUri}/{endpoint}" : overrideUri;

            var getRequest = useCredentials
                                 ? new JsonGetNetworkRequest(new HttpClient(), uri, this.GetRequestHeaders())
                                 : new JsonGetNetworkRequest(new HttpClient(), uri);


            return await getRequest.ExecuteAsync<TResponse>(cts);
        }

        private async Task<TResponse> PostAsync<TResponse>(
            string endpoint,
            object data,
            bool useCredentials = true,
            string overrideUri = null,
            CancellationTokenSource cts = null)
        {
            var uri = string.IsNullOrWhiteSpace(overrideUri) ? $"{BaseApiUri}/{endpoint}" : overrideUri;

            var json = SerializationService.Json.Serialize(data);

            var postRequest = useCredentials
                                  ? new JsonPostNetworkRequest(new HttpClient(), uri, json, this.GetRequestHeaders())
                                  : new JsonPostNetworkRequest(new HttpClient(), uri, json);

            return await postRequest.ExecuteAsync<TResponse>(cts);
        }

        private async Task<TResponse> PutAsync<TResponse>(
            string endpoint,
            object data,
            bool useCredentials = true,
            string overrideUri = null,
            CancellationTokenSource cts = null)
        {
            var uri = string.IsNullOrWhiteSpace(overrideUri) ? $"{BaseApiUri}/{endpoint}" : overrideUri;

            var json = SerializationService.Json.Serialize(data);

            var putRequest = useCredentials
                                 ? new JsonPutNetworkRequest(new HttpClient(), uri, json, this.GetRequestHeaders())
                                 : new JsonPutNetworkRequest(new HttpClient(), uri, json);

            return await putRequest.ExecuteAsync<TResponse>(cts);
        }

        private async Task<TResponse> DeleteAsync<TResponse>(
            string endpoint,
            bool useCredentials = true,
            string overrideUri = null,
            CancellationTokenSource cts = null)
        {
            var uri = string.IsNullOrWhiteSpace(overrideUri) ? $"{BaseApiUri}/{endpoint}" : overrideUri;

            var deleteRequest = useCredentials
                                    ? new JsonDeleteNetworkRequest(new HttpClient(), uri, this.GetRequestHeaders())
                                    : new JsonDeleteNetworkRequest(new HttpClient(), uri);

            return await deleteRequest.ExecuteAsync<TResponse>(cts);
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
                                      },
                                      { "Ocp-Apim-Subscription-Key", this.SubscriptionKey }
                                  };
                return headers;
            }

            return new Dictionary<string, string>();
        }
    }
}