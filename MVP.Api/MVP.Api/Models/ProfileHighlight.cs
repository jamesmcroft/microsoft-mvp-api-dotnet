namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class ProfileHighlight
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Date")]
        public DateTime? Date { get; set; }

        [JsonProperty("DateFormatted")]
        public string DateFormatted { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }
    }
}