namespace MVP.Api
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models;

    public partial class ApiClient
    {
        private const string OnlineIdentityEndpoint = "onlineidentities";

        /// <summary>
        /// Gets a list of online identities for the authenticated user.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A collection of <see cref="MVP.Api.Models.OnlineIdentity"/> objects.</returns>
        public async Task<IEnumerable<OnlineIdentity>> GetOnlineIdentitiesAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<OnlineIdentity>>(
                OnlineIdentityEndpoint,
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Gets a list of online identities by a nominations identifier for the authenticated user.
        /// </summary>
        /// <param name="nominationsId">The nominations identifier.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A collection of <see cref="MVP.Api.Models.OnlineIdentity"/> objects.</returns>
        public async Task<IEnumerable<OnlineIdentity>> GetOnlineIdentitiesByNominationsIdAsync(
            Guid nominationsId,
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<OnlineIdentity>>(
                $"{OnlineIdentityEndpoint}/{nominationsId}",
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Creates a new online identity.
        /// </summary>
        /// <param name="identity">The online identity to add.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The created online identity item.</returns>
        public async Task<OnlineIdentity> AddOnlineIdentityAsync(
            OnlineIdentity identity,
            CancellationToken cancellationToken = default)
        {
            return await this.PostAsync<OnlineIdentity>(
                OnlineIdentityEndpoint,
                identity,
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Updates an existing online identity.
        /// </summary>
        /// <param name="identity">The online identity to update.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>True if the online identity is updated successfully; otherwise, false.</returns>
        public async Task<bool> UpdateOnlineIdentityAsync(
            OnlineIdentity identity,
            CancellationToken cancellationToken = default)
        {
            return await this.PutAsync(
                OnlineIdentityEndpoint,
                identity,
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Deletes an online identity.
        /// </summary>
        /// <param name="id">The online identity identifier.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>True if the contribution is deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteOnlineIdentityAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await this.DeleteAsync(
                $"{OnlineIdentityEndpoint}?id={id}",
                true,
                null,
                cancellationToken);
        }
    }
}