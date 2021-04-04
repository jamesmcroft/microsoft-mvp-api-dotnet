namespace MVP.Api.Models
{
    using Newtonsoft.Json;

    public class ItemVisibility
    {
        [JsonProperty("Id")] public int? Id { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }

        [JsonProperty("LocalizeKey")] public string LocalizeKey { get; set; }
    }
}