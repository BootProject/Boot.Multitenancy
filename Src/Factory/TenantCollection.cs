using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Factory
{
    /// <summary>
    /// TenantCollection
    /// Contains a Collection of Tenants.
    /// </summary>
    public class TenantCollection : Dictionary<string, Tenant>, ITenantCollection
    {

        /// <summary>
        /// Add a Tenant to collection
        /// </summary>
        /// <param name="tenant">The Tenant to dd</param>
        public void Add(Tenant tenant)
        {
            base.Add(tenant.Configuration.Key, tenant);
        }
    }
}
