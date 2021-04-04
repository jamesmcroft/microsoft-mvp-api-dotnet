namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class ContributionTechnology
    {
        [JsonProperty("Id")] public Guid? Id { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("AwardName")] public string AwardName { get; set; }

        [JsonProperty("AwardCategory")] public string AwardCategory { get; set; }
    }
}