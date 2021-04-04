namespace MVP.Api.Models
{
    using System;

    using Newtonsoft.Json;

    public class ContributionType : IEquatable<ContributionType>
    {
        [JsonProperty("Id")] public Guid? Id { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("EnglishName")] public string EnglishName { get; set; }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ContributionType);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(ContributionType other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Nullable.Equals(this.Id, other.Id) && this.Name == other.Name &&
                   this.EnglishName == other.EnglishName;
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EnglishName != null ? this.EnglishName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}