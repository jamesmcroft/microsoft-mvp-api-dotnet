namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class Certification
    {
        [JsonProperty("Id")]
        public Guid? Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("PrivateSiteId")]
        public int? PrivateSiteId { get; set; }

        [JsonProperty("CertificationVisibility")]
        public ItemVisibility Visibility { get; set; }
    }
}