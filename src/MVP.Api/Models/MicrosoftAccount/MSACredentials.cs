namespace MVP.Api.Models.MicrosoftAccount
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines a model for stored credentials of a Microsoft Account.
    /// </summary>
    public class MSACredentials
    {
        /// <summary>
        /// Gets or sets the user's ID for the MSA.
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the token for accessing services for the MSA.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the token for refreshing the access token for the MSA.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the time until the access token expires.
        /// </summary>
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}