using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Factory
{

    /// <summary>
    /// Interface for TenantConfiguration
    /// </summary>
    public interface ITenantConfiguration
    {

        /// <summary>
        /// The unique Key for this ISessionFactory(Databasename is most common)
        /// </summary>
        string Key { get; set; }


        /// <summary>
        /// The type of database to use.
        /// </summary>
        DbType DbType { get; set; }


        /// <summary>
        /// The connectionstring
        /// </summary>
        string Connectionstring { get; set; }


        /// <summary>
        /// ISessionFactory
        /// </summary>
        ISessionFactory SessionFactory { get; set; }


        /// <summary>
        /// HostHeader values
        /// </summary>
        List<string> HostValues { get; set; }

        /// <summary>
        /// A ditionary of properties. Delimiter '|' Eg. Key,Value|Key,Value
        /// </summary>
        Dictionary<string, object> Properties { get; set; }
    }
}
