namespace MVP.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ContributionArea
    {
        [JsonProperty("AwardName")] public string AwardName { get; set; }

        [JsonProperty("ContributionArea")] public List<ActivityTechnology> Items { get; set; }
    }
}