namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class ActivityTechnology
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("AwardName")]
        public string AwardName { get; set; }

        [JsonProperty("AwardCategory")]
        public string AwardCategory { get; set; }

        [JsonProperty("Statuscode")]
        public int? StatusCode { get; set; }

        [JsonProperty("Active")]
        public bool? IsActive { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ActivityTechnology);
        }

        public bool Equals(ActivityTechnology other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id)
                   && (object.ReferenceEquals(this.Name, other.Name)
                       || this.Name != null && this.Name.Equals(other.Name))
                   && (object.ReferenceEquals(this.AwardName, other.AwardName)
                       || this.AwardName != null && this.AwardName.Equals(other.AwardName))
                   && (object.ReferenceEquals(this.AwardCategory, other.AwardCategory)
                       || this.AwardCategory != null && this.AwardCategory.Equals(other.AwardCategory))
                   && (object.ReferenceEquals(this.StatusCode, other.StatusCode)
                       || this.StatusCode != null && this.StatusCode.Equals(other.StatusCode))
                   && (object.ReferenceEquals(this.IsActive, other.IsActive)
                       || this.IsActive != null && this.IsActive.Equals(other.IsActive));
        }
    }
}