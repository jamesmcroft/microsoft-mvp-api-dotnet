namespace MVP.Api.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class AwardContribution
    {
        [JsonProperty("AwardCategory")] public string AwardCategory { get; set; }

        [JsonProperty("Contributions")] public List<ContributionArea> Areas { get; set; }
    }
}