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


        /// <summary>
        /// Validate hostheader values.
        /// Ensure they are unique.
        /// </summary>
        internal void Validate()
        {
            var list = new List<string>();
            var unique = new List<string>();
            var collection = ((TenantCollection)this).Values.ToList();
            collection.ForEach(t => { list.AddRange(t.Configuration.HostValues); });
            list.ForEach(t => {
                if (unique.Contains(t))
                    throw new TenantCollecionValidateException();
                unique.Add(t);
            });
        }
    }
}
