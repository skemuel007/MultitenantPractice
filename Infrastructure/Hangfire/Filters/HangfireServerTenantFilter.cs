using Core.Interfaces.Hangfire;
using Hangfire.Server;

namespace Infrastructure.Hangfire.Filters
{
    public class HangfireServerTenantFilter : IServerFilter
    {
        private readonly IHangfireTenantProvider _hangfireTenantProvider;

        public HangfireServerTenantFilter(IHangfireTenantProvider hangfireTenantProvider)
        {
            _hangfireTenantProvider = hangfireTenantProvider;
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }
        }

        public void OnPerforming(PerformingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            var tenantId = filterContext.GetJobParameter<string>("TenantId");
            _hangfireTenantProvider.HangfireSetTenant(tenantId);
        }
    }
}
