namespace MVP.Api
{
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models;

    public partial class ApiClient
    {
        private const string ProfileEndpoint = "profile";

        /// <summary>
        /// Gets the MVP profile for the authenticated user asynchronously.
        /// </summary>
        /// <param name="cts">
        /// Optional, a cancellation token source.
        /// </param>
        /// <returns>
        /// Returns the MVP profile.
        /// </returns>
        public async Task<MVPProfile> GetMyProfileAsync(CancellationTokenSource cts = null)
        {
            return await this.GetAsync<MVPProfile>(ProfileEndpoint, true, null, cts);
        }

        /// <summary>
        /// Gets the MVP profile image for the authenticated user as a base64 string asynchronously.
        /// </summary>
        /// <param name="cts">
        /// Optional, a cancellation token source.
        /// </param>
        /// <returns>
        /// Returns an image as a base64 string.
        /// </returns>
        public async Task<string> GetMyProfileImageAsync(CancellationTokenSource cts = null)
        {
            return await this.GetAsync<string>($"{ProfileEndpoint}/photo", true, null, cts);
        }

        /// <summary>
        /// Gets an MVP profile for another MVP by their MVP ID asynchronously.
        /// </summary>
        /// <param name="mvpId">
        /// The MVP ID, e.g. 5001534.
        /// </param>
        /// <param name="cts">
        /// Optional, a cancellation token source.
        /// </param>
        /// <returns>
        /// Returns the MVP profile.
        /// </returns>
        public async Task<MVPProfile> GetProfileAsync(string mvpId, CancellationTokenSource cts = null)
        {
            return await this.GetAsync<MVPProfile>($"{ProfileEndpoint}/{mvpId}", true, null, cts);
        }
    }
}