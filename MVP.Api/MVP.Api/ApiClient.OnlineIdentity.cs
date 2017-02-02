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

        public async Task<IEnumerable<OnlineIdentity>> GetOnlineIdentitiesAsync(CancellationTokenSource cts = null)
        {
            return await this.GetAsync<IEnumerable<OnlineIdentity>>(OnlineIdentityEndpoint, true, null, cts);
        }

        public async Task<IEnumerable<OnlineIdentity>> GetOnlineIdentitiesByNominationsIdAsync(
            Guid nominationsId,
            CancellationTokenSource cts = null)
        {
            return await this.GetAsync<IEnumerable<OnlineIdentity>>(
                       $"{OnlineIdentityEndpoint}/{nominationsId}",
                       true,
                       null,
                       cts);
        }

        public async Task<OnlineIdentity> GetOnlineIdentityByIdAsync(int id, CancellationTokenSource cts = null)
        {
            return await this.GetAsync<OnlineIdentity>($"{OnlineIdentityEndpoint}/{id}", true, null, cts);
        }

        public async Task<bool> DeleteOnlineIdentityAsync(int id, CancellationTokenSource cts = null)
        {
            return await this.DeleteAsync<bool>($"{OnlineIdentityEndpoint}?id={id}", true, null, cts);
        }
    }
}