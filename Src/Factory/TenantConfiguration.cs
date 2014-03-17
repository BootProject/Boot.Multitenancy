using FluentNHibernate.Cfg;
using System;
using System.Collections.Generic;

namespace Boot.Multitenancy.Factory
{

    /// <summary>
    /// TenantConfiguration <see cref="Boot.Multitenancy.Factory.ITenantConfiguration" />
    /// </summary>
    public class TenantConfiguration : ITenantConfiguration
    {
        public string Key { get; set; }
        public DbType DbType { get; set; }
        public string Connectionstring { get; set; }
        public List<string> HostValues { get; set; }
        public NHibernate.ISessionFactory SessionFactory { get; set; } //Todo makes the setter private.
        public Dictionary<string, object> Properties { get; set; }
    }
}
