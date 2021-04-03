namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class OnlineIdentityBase
    {
        [JsonProperty("OnlineIdentityId")]
        public Guid? Id { get; set; }

        [JsonProperty("MvpGuid")]
        public Guid? MvpId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("SocialNetwork")]
        public SocialNetwork SocialNetwork { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("MicrosoftAccount")]
        public string MicrosoftAccount { get; set; }

        [JsonProperty("ContributionCollected")]
        public bool? ContributionCollected { get; set; }

        [JsonProperty("PrivacyConsentStatus")]
        public bool? PrivacyConsentStatus { get; set; }

        [JsonProperty("PrivateSiteId")]
        public int? PrivateSiteId { get; set; }

        [JsonProperty("OnlineIdentityVisibility")]
        public ItemVisibility Visibility { get; set; }

        [JsonProperty("Submitted")]
        public bool? IsSubmitted { get; set; }
    }
}