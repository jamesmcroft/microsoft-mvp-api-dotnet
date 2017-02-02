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
        /// Gets the credentials associated with the logged in Microsoft account.
        /// </summary>
        public MSACredentials Credentials { get; private set; }

        private async Task<TResponse> GetAsync<TResponse>(string endpoint, CancellationTokenSource cts = null)
        {
            var getRequest = new JsonGetNetworkRequest(
                new HttpClient(),
                $"{BaseApiUri}/{endpoint}",
                this.GetRequestHeaders());

            return await getRequest.ExecuteAsync<TResponse>(cts);
        }

        private async Task<TResponse> PostAsync<TResponse>(
            string endpoint,
            object data,
            CancellationTokenSource cts = null)
        {
            var json = SerializationService.Json.Serialize(data);

            var postRequest = new JsonPostNetworkRequest(
                new HttpClient(),
                $"{BaseApiUri}/{endpoint}",
                json,
                this.GetRequestHeaders());

            return await postRequest.ExecuteAsync<TResponse>(cts);
        }

        private async Task<TResponse> PutAsync<TResponse>(
            string endpoint,
            object data,
            CancellationTokenSource cts = null)
        {
            var json = SerializationService.Json.Serialize(data);

            var putRequest = new JsonPutNetworkRequest(
                new HttpClient(),
                $"{BaseApiUri}/{endpoint}",
                json,
                this.GetRequestHeaders());

            return await putRequest.ExecuteAsync<TResponse>(cts);
        }

        private async Task<TResponse> DeleteAsync<TResponse>(string endpoint, CancellationTokenSource cts = null)
        {
            var deleteRequest = new JsonDeleteNetworkRequest(
                new HttpClient(),
                $"{BaseApiUri}/{endpoint}",
                this.GetRequestHeaders());

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