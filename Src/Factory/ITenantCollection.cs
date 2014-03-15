using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Factory
{
    public interface ITenantCollection
    {
        void Add(Tenant tenant);
    }
}
