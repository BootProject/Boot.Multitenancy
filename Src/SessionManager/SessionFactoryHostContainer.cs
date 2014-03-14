﻿using Boot.Multitenancy.Factory;
using NHibernate;
using System;
using System.Collections.Generic;
using Boot.Multitenancy.Extensions;

namespace Boot.Multitenancy.SessionManager
{
    /// <summary>
    /// Contains Multiple SessionFactiories
    /// </summary>
    public class SessionFactoryHostContainer
    {

        /// <summary>
        /// The current SessionFactoryHostContainer.
        /// </summary>
        public static SessionFactoryHostContainer Current { get; private set; }
        private static readonly object Lock = new object();
        internal Dictionary<string, ITenant> SessionFactories { get; set; }


        /// <summary>
        /// Get Current ISessionFactory
        /// </summary>
        public static ISessionFactory CurrentFactory
        {
            get
            {
                return Current.TenantConfiguration.SessionFactory;
            }
        }


        /// <summary>
        /// Get Current TenantConfiguration
        /// </summary>
        public ITenantConfiguration TenantConfiguration
        {
            get
            {
                foreach (var item in Current.SessionFactories.Values)
                {
                    var tenant = (Tenant)item;
                    var hostValues = tenant.Configuration.HostValues;
                    foreach (var val in hostValues)
                    {
                        if (val.Equals(string.Empty.GetDomain()))
                            return tenant.Configuration;
                    }
                }
                return null;
            }
        }


        /// <summary>
        /// Add a Tenant
        /// </summary>
        /// <param name="tenant">The Tenant to add</param>
        /// <returns>ISessionFactoryHostContainer</returns>
        internal SessionFactoryHostContainer Add(Tenant tenant)
        {
            if (tenant.Equals(null))
                throw new Exception("Tenant provided is null.");

            if (Current.SessionFactories.ContainsKey(tenant.Configuration.Key).Equals(false)) {
                Current.SessionFactories.Add(tenant.Configuration.Key, tenant);
            }
   
            return Current;
        }


        /// <summary>
        /// Singleton SessionFactoryHostContainer
        /// </summary>
        static SessionFactoryHostContainer()
        {
            lock (Lock)
            {
                Current = new SessionFactoryHostContainer();
            }
        }


        /// <summary>
        /// Singleton SessionFactoryHostContainer
        /// </summary>
        private SessionFactoryHostContainer()
        {
            SessionFactories = new Dictionary<string, ITenant>();
        }
    }
}