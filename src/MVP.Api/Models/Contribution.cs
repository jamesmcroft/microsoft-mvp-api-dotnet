namespace MVP.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Contribution
    {
        [JsonProperty("ContributionId")] public int? Id { get; set; }

        [JsonProperty("ContributionTypeName")] public string TypeName { get; set; }

        [JsonProperty("ContributionType")] public ContributionType Type { get; set; }

        [JsonProperty("ContributionTechnology")]
        public ContributionTechnology Technology { get; set; }

        [JsonProperty("AdditionalTechnologies")]
        public IList<ContributionTechnology> AdditionalTechnologies { get; set; } = new List<ContributionTechnology>();

        [JsonProperty("StartDate")] public DateTime? StartDate { get; set; }

        [JsonProperty("Title")] public string Title { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }

        [JsonProperty("AnnualQuantity")] public int? AnnualQuantity { get; set; }

        [JsonProperty("SecondAnnualQuantity")] public int? SecondAnnualQuantity { get; set; }

        [JsonProperty("AnnualReach")] public int? AnnualReach { get; set; }

        [JsonProperty("ReferenceUrl")] public string ReferenceUrl { get; set; }

        [JsonProperty("Visibility")] public ItemVisibility Visibility { get; set; }
    }
}