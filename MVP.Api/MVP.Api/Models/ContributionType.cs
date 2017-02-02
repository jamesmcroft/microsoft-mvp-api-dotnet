namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class ContributionType
    {
        [JsonProperty("Id")]
        public Guid? Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("EnglishName")]
        public string EnglishName { get; set; }
    }
}