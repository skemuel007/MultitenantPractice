using Core.Interfaces.Hangfire;
using Hangfire;
using Hangfire.Common;

namespace Infrastructure.Hangfire.Providers
{
    public class HangfireServerFilterProvider : IJobFilterProvider
    {
        private readonly IHangfireTenantProvider _hangfireTenantProvider;

        public HangfireServerFilterProvider(IHangfireTenantProvider hangfireTenantProvider)
        {
            _hangfireTenantProvider = hangfireTenantProvider;
        }
        public IEnumerable<JobFilter> GetFilters(Job job)
        {
            return new JobFilter[]
            {
                new JobFilter(new CaptureCultureAttribute(), JobFilterScope.Global, null),
                new JobFilter(new HangfireClientTenantFilter(_tenantService), JobFilterScope.Global, null),
            };
        }
    }
}
