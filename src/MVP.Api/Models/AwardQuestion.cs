namespace MVP.Api.Models
{
    using System;
    using Newtonsoft.Json;

    public class AwardQuestion
    {
        [JsonProperty("AwardQuestionId")] public Guid? AwardQuestionId { get; set; }

        [JsonProperty("QuestionContent")] public string Question { get; set; }

        [JsonProperty("Required")] public bool Required { get; set; }
    }
}