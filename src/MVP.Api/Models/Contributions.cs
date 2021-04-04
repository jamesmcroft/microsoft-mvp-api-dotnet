namespace MVP.Api.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Contributions
    {
        [JsonProperty("Contributions")] public IList<Contribution> Items { get; set; }

        [JsonProperty("TotalContributions")] public int? AvailableCount { get; set; }

        [JsonProperty("PagingIndex")] public int? PageIndex { get; set; }

        public int PageSize { get; set; }

        public int? TotalPages { get; set; }

        internal void EvaluatePagination(int pageSize)
        {
            this.PageSize = pageSize;
            this.TotalPages = this.AvailableCount == default || this.AvailableCount == 0 || pageSize == 0
                ? 0
                : this.AvailableCount / pageSize;
        }
    }
}