namespace MVP.Api
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MVP.Api.Models;

    public partial class ApiClient
    {
        private const string ContributionEndpoint = "contributions";

        public async Task<IEnumerable<AwardContribution>> GetContributionAreasAsync(CancellationTokenSource cts = null)
        {
            return await this.GetAsync<IEnumerable<AwardContribution>>(
                       $"{ContributionEndpoint}/contributionareas",
                       true,
                       null,
                       cts);
        }

        public async Task<Contribution> GetContributionByIdAsync(int id, CancellationTokenSource cts = null)
        {
            return await this.GetAsync<Contribution>($"{ContributionEndpoint}/{id}", true, null, cts);
        }

        public async Task<Contributions> GetContributionsAsync(
            int offset,
            int limit,
            CancellationTokenSource cts = null)
        {
            return await this.GetAsync<Contributions>($"{ContributionEndpoint}/{offset}/{limit}", true, null, cts);
        }

        public async Task<IEnumerable<ContributionType>> GetContributionTypesAsync(CancellationTokenSource cts = null)
        {
            return await this.GetAsync<IEnumerable<ContributionType>>(
                       $"{ContributionEndpoint}/contributiontypes",
                       true,
                       null,
                       cts);
        }

        public async Task<Contribution> AddContributionAsync(
            Contribution contribution,
            CancellationTokenSource cts = null)
        {
            return await this.PostAsync<Contribution>(ContributionEndpoint, contribution, true, null, cts);
        }

        public async Task<bool> UpdateContributionAsync(
            Contribution contribution,
            CancellationTokenSource cts = null)
        {
            return await this.PutAsync(ContributionEndpoint, contribution, true, null, cts);
        }

        public async Task<bool> DeleteContributionAsync(int id, CancellationTokenSource cts = null)
        {
            return await this.DeleteAsync($"{ContributionEndpoint}?id={id}", true, null, cts);
        }
    }
}