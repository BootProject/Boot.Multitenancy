﻿using System.Collections.Generic;
using Boot.Multitenancy.Factory;
using Boot.Multitenancy.SessionManager;
using Boot.Multitenancy.Configuration;
using System.Collections;
using System.Linq;
using Boot.Multitenancy.Extensions;
using Con = Boot.Multitenancy.Configuration.ConnectionstringConfiguration;
using Conf = Boot.Multitenancy;
using System;
using System.Text;

namespace Boot.Multitenancy
{

    /// <summary>
    /// The default Host factory. Creates Tenants.
    /// </summary>
    public class Host : IHost
    {

        private static readonly object Lock = new object();
        public static Host FactoryHost { get; private set; }
        internal TenantCollection Tenants { get; set; }


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
            Tenants = new TenantCollection();
        }


        /// <summary>
        /// Init configuration from web.config
        /// </summary>
        public static void Init()
        {
            bool noTenants = (FactoryHost.Tenants != null);
            bool useConfig = CreateEnvironment();

            if (noTenants && useConfig) //See if tenants already created.
                CreateTenants();

            Init(FactoryHost.Tenants);
        }


        /// <summary>
        /// Intit configuration with a List of Tenants
        /// </summary>
        /// <param name="tenants">TenantCollection to add</param>
        public static void Init(TenantCollection tenants)
        {
            FactoryHost.Tenants = tenants;
            FactoryHost.Tenants.Validate();

            (from tenant in FactoryHost.Tenants 
              select tenant)
                .ToList()
                    .ForEach(t =>  {
                        var tenant = (Tenant)t.Value;
                        tenant.Configuration.SessionFactory = tenant.CreateConfig();

                        if (tenant.Configuration.HostValues == null)
                            tenant.Configuration.HostValues = new List<string>();

                        if (tenant.Configuration.Properties == null)
                            tenant.Configuration.Properties = new Dictionary<string, object>();
                            
                        SessionFactoryHostContainer.Current.Add(tenant);
                    });
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

                var tenant = new Tenant() { Configuration = conf };
                FactoryHost.Tenants.Add(element.Name, tenant);
            }
        }


        /// <summary>
        /// Reads out configuration before creating FluentConfiguration.
        /// Does not affect the created collection.
        /// </summary>
        /// <returns>A list of ConnectionElement</returns>
        public static TenantCollection ConfigCollection
        {
            get{
                var tenants = new TenantCollection();
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

                    var tenant = new Tenant() { Configuration = conf };
                    tenants.Add(element.Name, tenant);
                }
                return tenants;
            }
        }


        /// <summary>
        /// Varaiable to check for setup.
        /// </summary>
        /// <returns></returns>
        private static bool CreateEnvironment()
        {
            return Configuration.Persist;
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
