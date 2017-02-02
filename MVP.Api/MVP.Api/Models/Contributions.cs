namespace MVP.Api.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Contributions
    {
        [JsonProperty("Contributions")]
        public IList<Contribution> Items { get; set; }

        [JsonProperty("TotalContributions")]
        public int? Total { get; set; }

        [JsonProperty("PagingIndex")]
        public int? PageIdx { get; set; }
    }
}