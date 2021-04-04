namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class SocialNetwork
    {
        [JsonProperty("Id")] public Guid Id { get; set; }

        [JsonProperty("SocialNetworkId")] public Guid? SocialNetworkId { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("Website")] public string Website { get; set; }

        [JsonProperty("IconUrl")] public string IconUrl { get; set; }

        [JsonProperty("StatusCode")] public SocialNetworkStatusCode StatusCode { get; set; }

        [JsonProperty("SystemCollectionEnabled")]
        public bool? SystemCollectionEnabled { get; set; }
    }
}