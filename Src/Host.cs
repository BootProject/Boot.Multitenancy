using System.Collections.Generic;
using Boot.Multitenancy.Factory;
using Boot.Multitenancy.SessionManager;
using Boot.Multitenancy.Configuration;
using System.Collections;
using System.Linq;
using Boot.Multitenancy.Extensions;
using Con = Boot.Multitenancy.Configuration.ConnectionstringConfiguration;
using Conf = Boot.Multitenancy;

namespace Boot.Multitenancy
{

    /// <summary>
    /// The default Host factory. Creates Tenants.
    /// </summary>
    public class Host : IHost
    {

        private static readonly object Lock = new object();
        internal static IHost FactoryHost { get; private set; }
        public static Dictionary<string, ITenant> Tenants { get; set; }


        /// <summary>
        /// Static Ctor
        /// </summary>
        static Host()
        {
            lock (Lock)
            {
                FactoryHost = new Host();
            }
        }

        /// <summary>
        /// Singleton implementation
        /// </summary>
        private Host()
        {
            Tenants = new Dictionary<string, ITenant>();
        }


        /// <summary>
        /// Init configuration from web.config
        /// </summary>
        public static void Init()
        {
            CreateTenants();
            Init(Tenants);
        }


        /// <summary>
        /// Intit configuration with a List of Tenants
        /// </summary>
        /// <param name="tenants"></param>
        public static void Init(Dictionary<string, ITenant> tenants)
        {
            foreach (var tenant in tenants.ToList()) 
            {
                var _tenant = (Tenant)tenant.Value;
                    _tenant.Configuration.SessionFactory = _tenant.CreateConfig();

                if (_tenant.Configuration.HostValues == null)
                    _tenant.Configuration.HostValues = new List<string>();

                if (_tenant.Configuration.Properties == null)
                    _tenant.Configuration.Properties = new Dictionary<string, object>();

                SessionFactoryHostContainer.Current.Add(_tenant);
            }
        }


        /// <summary>
        /// Builds the configuration from web.config
        /// </summary>
        private static void CreateTenants()
        {
            foreach (var element in Databases) 
            {
                var conf = new TenantConfiguration
                {
                    Key = element.Name,
                    DbType = element.DbType,
                    HostValues = element.DomainList,
                    Properties = element.PropertyList,
                    Connectionstring = element.Connectionstring
                };

                var tenant = new Tenant(conf);
                Tenants.Add(element.Name, tenant);
            }
        }


        /// <summary>
        /// A set of databases listed from web.config
        /// </summary>
        private static List<DatabaseSection> Databases
        {
            get { return ConvertToList(Collection); }
        }



        /// <summary>
        /// Convert a Collection to a List<T>
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        private static List<DatabaseSection> ConvertToList(ICollection col)
        {
            return Collection.CollectionToList<DatabaseSection>();
        }



        /// <summary>
        /// The DatabaseCollection. 
        /// </summary>
        private static DatabaseCollection Collection
        {
            get { return Configuration.Databases; }
        }


        /// <summary>
        /// Get the SessionFactoryConfiguration
        /// </summary>
        private static SessionFactoryConfiguration Configuration
        {
            get { return Conf.Configuration.DatabaseCollectionReader.conf; }
        }
    }
}
