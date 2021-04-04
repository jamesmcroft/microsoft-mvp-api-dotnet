namespace MVP.Api
{
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models;

    public partial class ApiClient
    {
        private const string ProfileEndpoint = "profile";

        /// <summary>
        /// Gets the profile details for the authenticated user.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A <see cref="MVPProfile"/> object.</returns>
        public async Task<MVPProfile> GetMyProfileAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<MVPProfile>(
                ProfileEndpoint,
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Gets the profile image for the authenticated user as a base64 string.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A base64 string containing the image data.</returns>
        public async Task<string> GetMyProfileImageAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<string>(
                $"{ProfileEndpoint}/photo",
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Gets the profile details for another MVP.
        /// </summary>
        /// <param name="mvpId">The MVP ID, e.g. 5001534.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A <see cref="MVPProfile"/> object.</returns>
        public async Task<MVPProfile> GetProfileAsync(
            string mvpId,
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<MVPProfile>(
                $"{ProfileEndpoint}/{mvpId}",
                true,
                null,
                cancellationToken);
        }
    }
}