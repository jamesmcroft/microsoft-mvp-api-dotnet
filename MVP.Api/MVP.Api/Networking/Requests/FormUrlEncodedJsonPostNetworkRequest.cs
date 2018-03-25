namespace MVP.Api.Networking.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using MADE.Networking.Requests.Json;

    public class FormUrlEncodedJsonPostNetworkRequest : JsonResponseNetworkRequest
    {
        public FormUrlEncodedJsonPostNetworkRequest(string url, FormUrlEncodedContent data, Type responseType)
            : this(url, null, data, responseType)
        {
        }

        public FormUrlEncodedJsonPostNetworkRequest(
            string url,
            Dictionary<string, string> headers,
            FormUrlEncodedContent requestContent,
            Type responseType)
            : base(url, HttpMethod.Post, headers, responseType)
        {
            this.RequestContent = requestContent;
        }
    }
}