namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class OnlineIdentity : OnlineIdentityBase
    {
        [JsonProperty("PrivacyConsentCheckStatus")]
        public bool? PrivacyConsentCheckStatus { get; set; }

        [JsonProperty("PrivacyConsentCheckDate")]
        public DateTime? PrivacyConsentCheckDate { get; set; }

        [JsonProperty("PrivacyConsentUnCheckDate")]
        public DateTime? PrivacyConsentUnCheckDate { get; set; }
    }
}