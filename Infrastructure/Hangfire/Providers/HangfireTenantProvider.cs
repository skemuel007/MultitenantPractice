using Core.Interfaces.Hangfire;

namespace Infrastructure.Hangfire.Providers
{
    public class HangfireTenantProvider : IHangfireTenantProvider
    {
        private static AsyncLocal<string> HangfireTenantId { get; } = new AsyncLocal<string>();
        public string HangfireGetTenantId()
        {
            return HangfireTenantId.Value;
        }

        public void HangfireSetTenant(string tenantId)
        {
            HangfireTenantId.Value = tenantId;
        }
    }
}
