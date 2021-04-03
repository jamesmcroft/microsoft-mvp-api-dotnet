namespace MVP.Api.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class AwardContribution
    {
        [JsonProperty("AwardCategory")]
        public string AwardCategory { get; set; } 

        [JsonProperty("Contributions")]
        public List<ContributionArea> Areas { get; set; }
    }

    public class ContributionArea
    {
        [JsonProperty("AwardName")]
        public string AwardName { get; set; }

        [JsonProperty("ContributionArea")]
        public List<ActivityTechnology> Items { get; set; }
    }
}
