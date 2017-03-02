namespace MVP.Api.Networking.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using WinUX.Data.Serialization;
    using WinUX.Networking.Requests;

    public class FormUrlEncodedJsonPostNetworkRequest : NetworkRequest
    {
        private readonly HttpClient client;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public FormUrlEncodedContent Data { get; set; }

        public FormUrlEncodedJsonPostNetworkRequest(HttpClient client, string url)
            : this(client, url, null, null)
        {
        }

        public FormUrlEncodedJsonPostNetworkRequest(HttpClient client, string url, FormUrlEncodedContent data)
            : this(client, url, data, null)
        {
        }

        public FormUrlEncodedJsonPostNetworkRequest(
            HttpClient client,
            string url,
            FormUrlEncodedContent data,
            Dictionary<string, string> headers)
            : base(url, headers)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.client = client;
            this.Data = data;
        }

        public override async Task<TResponse> ExecuteAsync<TResponse>(CancellationTokenSource cts = null)
        {
            return SerializationService.Json.Deserialize<TResponse>(await this.GetJsonResponse(cts));
        }

        public override async Task<object> ExecuteAsync(Type expectedResponse, CancellationTokenSource cts = null)
        {
            return SerializationService.Json.Deserialize(await this.GetJsonResponse(cts), expectedResponse);
        }

        private async Task<string> GetJsonResponse(CancellationTokenSource cts = null)
        {
            if (this.client == null)
            {
                throw new InvalidOperationException(
                    "No HttpClient has been specified for executing the network request.");
            }

            if (string.IsNullOrWhiteSpace(this.Url))
            {
                throw new InvalidOperationException("No URL has been specified for executing the network request.");
            }

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(this.Url)) { Content = this.Data };
            if (this.Headers != null)
            {
                foreach (var header in this.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var response = cts == null
                               ? await this.client.PostAsync(new Uri(this.Url), this.Data)
                               : await this.client.PostAsync(new Uri(this.Url), this.Data, cts.Token);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}