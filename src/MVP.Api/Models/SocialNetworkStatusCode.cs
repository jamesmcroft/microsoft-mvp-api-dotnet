namespace MVP.Api.Models
{
    using Newtonsoft.Json;

    public class SocialNetworkStatusCode
    {
        [JsonProperty("Id")]
        public int? Id { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}