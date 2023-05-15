using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Hangfire
{
    public interface IHangfireTenantProvider
    {
        void HangfireSetTenant(string tenantId);
        string HangfireGetTenantId();
    }
}
