namespace MVP.Api
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models;

    public partial class ApiClient
    {
        private const string ContributionEndpoint = "contributions";

        /// <summary>
        /// Gets a list of contribution areas grouped by award names.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A collection of <see cref="AwardContribution"/> objects.</returns>
        public async Task<IEnumerable<AwardContribution>> GetContributionAreasAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<AwardContribution>>(
                $"{ContributionEndpoint}/contributionareas",
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Gets a contribution by an identifier.
        /// </summary>
        /// <param name="id">The contribution identifier.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A <see cref="Contribution"/> object.</returns>
        public async Task<Contribution> GetContributionByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<Contribution>(
                $"{ContributionEndpoint}/{id}",
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Gets a paginated list of contributions.
        /// </summary>
        /// <param name="offset">The page skip.</param>
        /// <param name="limit">The page take.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A <see cref="Contributions"/> object.</returns>
        public async Task<Contributions> GetContributionsAsync(
            int offset,
            int limit,
            CancellationToken cancellationToken = default)
        {
            Contributions result = await this.GetAsync<Contributions>(
                $"{ContributionEndpoint}/{offset}/{limit}",
                true,
                null,
                cancellationToken);
            result.EvaluatePagination(limit);
            return result;
        }

        /// <summary>
        /// Gets a list of contribution types.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A collection of <see cref="ContributionType"/> objects.</returns>
        public async Task<IEnumerable<ContributionType>> GetContributionTypesAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<ContributionType>>(
                $"{ContributionEndpoint}/contributiontypes",
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Creates a new contribution.
        /// </summary>
        /// <param name="contribution">The contribution to add.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The created contribution item.</returns>
        public async Task<Contribution> AddContributionAsync(
            Contribution contribution,
            CancellationToken cancellationToken = default)
        {
            return await this.PostAsync<Contribution>(
                ContributionEndpoint,
                contribution,
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Updates an existing contribution.
        /// </summary>
        /// <param name="contribution">The contribution to update.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>True if the contribution is updated successfully; otherwise, false.</returns>
        public async Task<bool> UpdateContributionAsync(
            Contribution contribution,
            CancellationToken cancellationToken = default)
        {
            return await this.PutAsync(
                ContributionEndpoint,
                contribution,
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Deletes a contribution.
        /// </summary>
        /// <param name="id">The contribution identifier.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>True if the contribution is deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteContributionAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await this.DeleteAsync(
                $"{ContributionEndpoint}?id={id}",
                true,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Gets the available sharing preferences for contributions.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>A collection of <see cref="ItemVisibility"/> objects.</returns>
        public async Task<IEnumerable<ItemVisibility>> GetSharingPreferencesAsync(
            CancellationToken cancellationToken = default)
        {
            return await this.GetAsync<IEnumerable<ItemVisibility>>(
                $"{ContributionEndpoint}/sharingpreferences",
                true,
                null,
                cancellationToken);
        }
    }
}