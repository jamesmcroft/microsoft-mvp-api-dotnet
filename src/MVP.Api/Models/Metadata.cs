namespace MVP.Api.Models
{
    using Newtonsoft.Json;

    public class Metadata
    {
        [JsonProperty("PageTitle")] public string PageTitle { get; set; }

        [JsonProperty("TemplateName")] public string TemplateName { get; set; }

        [JsonProperty("Keywords")] public string Keywords { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }
    }
}