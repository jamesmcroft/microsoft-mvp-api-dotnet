namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class ActivityTechnology : IEquatable<ActivityTechnology>
    {
        [JsonProperty("Id")] public Guid Id { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("AwardName")] public string AwardName { get; set; }

        [JsonProperty("AwardCategory")] public string AwardCategory { get; set; }

        [JsonProperty("Statuscode")] public int? StatusCode { get; set; }

        [JsonProperty("Active")] public bool? IsActive { get; set; }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ActivityTechnology);
        }

        public bool Equals(ActivityTechnology other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Id.Equals(other.Id) && this.Name == other.Name && this.AwardName == other.AwardName &&
                   this.AwardCategory == other.AwardCategory && this.StatusCode == other.StatusCode &&
                   this.IsActive == other.IsActive;
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AwardName != null ? this.AwardName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AwardCategory != null ? this.AwardCategory.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.StatusCode.GetHashCode();
                hashCode = (hashCode * 397) ^ this.IsActive.GetHashCode();
                return hashCode;
            }
        }
    }
}