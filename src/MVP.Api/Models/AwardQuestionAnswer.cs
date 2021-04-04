namespace MVP.Api.Models
{
    using System;
    using Newtonsoft.Json;

    public class AwardQuestionAnswer
    {
        [JsonProperty("AwardQuestionId")] public Guid? AwardQuestionId { get; set; }

        [JsonProperty("Answer")] public string Answer { get; set; }
    }
}