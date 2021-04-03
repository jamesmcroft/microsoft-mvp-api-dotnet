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

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ContributionType);
        }

        public bool Equals(ContributionType other)
        {
            if (other == null) return false;

            return this.Id.Equals(other.Id)
                   && (object.ReferenceEquals(this.Name, other.Name)
                       || this.Name != null && this.Name.Equals(other.Name))
                   && (object.ReferenceEquals(this.EnglishName, other.EnglishName)
                       || this.EnglishName != null && this.EnglishName.Equals(other.EnglishName));
        }
    }
}