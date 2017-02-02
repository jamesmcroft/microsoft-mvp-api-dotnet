namespace MVP.Api.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class MVPProfile
    {
        [JsonProperty("Metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("MvpId")]
        public string MVPId { get; set; }

        [JsonProperty("YearsAsMvp")]
        public int? YearsAsMvp { get; set; }

        [JsonProperty("FirstAwardYear")]
        public string FirstAwardYear { get; set; }

        [JsonProperty("AwardCategoryDisplay")]
        public string AwardCategoryDisplay { get; set; }

        [JsonProperty("TechnicalExpertise")]
        public string TechnicalExpertise { get; set; }

        [JsonProperty("InTheSpotlight")]
        public bool? IsInSpotlight { get; set; }

        [JsonProperty("Headline")]
        public string Headline { get; set; }

        [JsonProperty("Biography")]
        public string Biography { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("PrimaryEmailAddress")]
        public string PrimaryEmailAddress { get; set; }

        [JsonProperty("ShippingCountry")]
        public string Country { get; set; }

        [JsonProperty("ShippingStateCity")]
        public string CityState { get; set; }

        [JsonProperty("Languages")]
        public string Languages { get; set; }

        [JsonProperty("OnlineIdentities")]
        public IList<OnlineIdentity> OnlineIdentities { get; set; }

        [JsonProperty("Certifications")]
        public IList<Certification> Certifications { get; set; }

        [JsonProperty("Activities")]
        public IList<Activity> Activities { get; set; }

        [JsonProperty("CommunityAwards")]
        public IList<AwardRecognition> CommunityAwards { get; set; }

        [JsonProperty("NewsHighlights")]
        public IList<ProfileHighlight> NewsHighlights { get; set; }

        [JsonProperty("UpcomingEvent")]
        public IList<ProfileHighlight> UpcomingEvent { get; set; }
    }
}