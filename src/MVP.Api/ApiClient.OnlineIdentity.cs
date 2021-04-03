﻿namespace MVP.Api
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

        public async Task<OnlineIdentityBase> AddOnlineIdentityAsync(
            OnlineIdentity identity,
            CancellationTokenSource cts = null)
        {
            return await this.PostAsync<OnlineIdentityBase>(OnlineIdentityEndpoint, identity, true, null, cts);
        }

        public async Task<bool> UpdateOnlineIdentityAsync(OnlineIdentity identity, CancellationTokenSource cts = null)
        {
            return await this.PutAsync(OnlineIdentityEndpoint, identity, true, null, cts);
        }

        public async Task<bool> DeleteOnlineIdentityAsync(int id, CancellationTokenSource cts = null)
        {
            return await this.DeleteAsync($"{OnlineIdentityEndpoint}?id={id}", true, null, cts);
        }
    }
}