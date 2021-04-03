namespace MVP.Api.Networking.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using MADE.App.Networking.Requests;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines a network request for a POST call using FormUrlEncodedContent with a JSON response.
    /// </summary>
    public class FormUrlEncodedJsonPostNetworkRequest : NetworkRequest
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUrlEncodedJsonPostNetworkRequest" /> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="T:System.Net.Http.HttpClient" /> for executing the request.
        /// </param>
        /// <param name="url">The URL for the request.</param>
        public FormUrlEncodedJsonPostNetworkRequest(HttpClient client, string url)
            : this(client, url, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUrlEncodedJsonPostNetworkRequest" /> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="T:System.Net.Http.HttpClient" /> for executing the request.
        /// </param>
        /// <param name="url">The URL for the request.</param>
        /// <param name="jsonData">The JSON data to post.</param>
        public FormUrlEncodedJsonPostNetworkRequest(HttpClient client, string url, FormUrlEncodedContent jsonData)
            : this(client, url, jsonData, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUrlEncodedJsonPostNetworkRequest" /> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="T:System.Net.Http.HttpClient" /> for executing the request.
        /// </param>
        /// <param name="url">The URL for the request.</param>
        /// <param name="jsonData">The JSON data to post.</param>
        /// <param name="headers">The additional headers.</param>
        public FormUrlEncodedJsonPostNetworkRequest(
            HttpClient client,
            string url,
            FormUrlEncodedContent jsonData,
            Dictionary<string, string> headers)
            : base(url, headers)
        {
            HttpClient httpClient = client;

            this.client = httpClient ?? throw new ArgumentNullException(nameof(client));
            this.Data = jsonData;
        }

        /// <summary>Gets or sets the data.</summary>
        public FormUrlEncodedContent Data { get; set; }

        /// <summary>Executes the network request.</summary>
        /// <param name="cts">The cancellation token source.</param>
        /// <typeparam name="TResponse">
        /// The type of object returned from the request.
        /// </typeparam>
        /// <returns>
        /// Returns the response of the request as the specified type.
        /// </returns>
        public override async Task<TResponse> ExecuteAsync<TResponse>(CancellationTokenSource cts = null)
        {
            return JsonConvert.DeserializeObject<TResponse>(await this.GetJsonResponse(cts));
        }

        /// <summary>Executes the network request.</summary>
        /// <param name="expectedResponse">
        /// The type expected by the response of the request.
        /// </param>
        /// <param name="cts">The cancellation token source.</param>
        /// <returns>Returns the response of the request as an object.</returns>
        public override async Task<object> ExecuteAsync(Type expectedResponse, CancellationTokenSource cts = null)
        {
            return JsonConvert.DeserializeObject(await this.GetJsonResponse(cts), expectedResponse);
        }

        private async Task<string> GetJsonResponse(CancellationTokenSource cts)
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

            Uri uri = new Uri(this.Url);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = this.Data };

            if (this.Headers != null)
            {
                foreach (KeyValuePair<string, string> header in this.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage response = cts == null
                                               ? await this.client.SendAsync(
                                                     request,
                                                     HttpCompletionOption.ResponseHeadersRead)
                                               : await this.client.SendAsync(
                                                     request,
                                                     HttpCompletionOption.ResponseHeadersRead,
                                                     cts.Token);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}