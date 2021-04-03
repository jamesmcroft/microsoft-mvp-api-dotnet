namespace MVP.Api.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Activity
    {
        [JsonProperty("TitleOfActivity")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("ActivityType")]
        public ActivityType Type { get; set; }

        [JsonProperty("ApplicableTechnology")]
        public ActivityTechnology Technology { get; set; }

        [JsonProperty("DateOfActivity")]
        public DateTime DateOfActivity { get; set; }

        [JsonProperty("DateOfActivityFormatted")]
        public string DateOfActivityFormatted { get; set; }

        [JsonProperty("EndDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("EndDateFormatted")]
        public string EndDateFormatted { get; set; }

        [JsonProperty("PrivateSiteId")]
        public int? PrivateSiteId { get; set; }

        [JsonProperty("ReferenceUrl")]
        public string ReferenceUrl { get; set; }

        [JsonProperty("ActivityVisibility")]
        public ItemVisibility Visibility { get; set; }

        [JsonProperty("AnnualQuantity")]
        public int? AnnualQuantity { get; set; }

        [JsonProperty("SecondAnnualQuantity")]
        public int? SecondAnnualQuantity { get; set; }

        [JsonProperty("AnnualReach")]
        public int? AnnualReach { get; set; }

        [JsonProperty("OnlineIdentity")]
        public OnlineIdentity OnlineIdentity { get; set; }

        [JsonProperty("SocialNetwork")]
        public SocialNetwork SocialNetwork { get; set; }

        [JsonProperty("AllAnswersUrl")]
        public string AllAnswersUrl { get; set; }

        [JsonProperty("AllPostsUrl")]
        public string AllPostsUrl { get; set; }

        [JsonProperty("IsSystemCollected")]
        public bool? IsSystemCollected { get; set; }

        [JsonProperty("IsBelongToLatestAwardCycle")]
        public bool? IsBelongToLatestAwardCycle { get; set; }

        [JsonProperty("DisplayMode")]
        public string DisplayMode { get; set; }

        [JsonProperty("ChartColumnIndexes")]
        public IList<int?> ChartColumnIndexes { get; set; }

        [JsonProperty("DescriptionSummaryFormat")]
        public string DescriptionSummaryFormat { get; set; }

        [JsonProperty("DataTableTitle")]
        public string DataTableTitle { get; set; }

        [JsonProperty("SubtitleHeader")]
        public string SubtitleHeader { get; set; }

        [JsonProperty("IsAllowEdit")]
        public bool? AllowEdit { get; set; }

        [JsonProperty("IsAllowDelete")]
        public bool? AllowDelete { get; set; }

        [JsonProperty("IsFromBookmarklet")]
        public bool? FromBookmarklet { get; set; }

        [JsonProperty("Submitted")]
        public bool? IsSubmitted { get; set; }
    }
}