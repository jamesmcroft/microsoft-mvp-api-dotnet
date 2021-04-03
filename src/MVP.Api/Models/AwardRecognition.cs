namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class AwardRecognition
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("DateEarned")]
        public DateTime DateEarned { get; set; }

        [JsonProperty("PrivateSiteId")]
        public int? PrivateSiteId { get; set; }

        [JsonProperty("ReferenceUrl")]
        public string ReferenceUrl { get; set; }

        [JsonProperty("AwardRecognitionVisibility")]
        public ItemVisibility Visibility { get; set; }
    }
}