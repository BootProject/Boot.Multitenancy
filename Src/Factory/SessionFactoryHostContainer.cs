﻿using System.Collections.Generic;
using System.Linq;
using Boot.Multitenancy.Extensions;
using FluentNHibernate.Conventions;
using NHibernate;
using System;
using System.Text;

namespace Boot.Multitenancy
{
    /// <summary>
    /// Contains methods to read multiple domains from a single SessionFactory object.
    /// </summary>
    public class SessionFactoryHostContainer : IDisposable
    {


        /// <summary>
        /// The current SessionFactoryHostContainer.
        /// </summary>
        public static SessionFactoryHostContainer Current { get; private set; }



        private static readonly object Lock = new object();
        internal Dictionary<string, SessionFactoryData> SessionFactories { get; set; }




        //Static Ctor
        static SessionFactoryHostContainer()
        {
            lock (Lock) {
                Current = new SessionFactoryHostContainer();
            }
        }



        /// <summary>
        /// Get the Current ISessionFactory
        /// </summary>
        public static ISessionFactory CurrentFactory
        {
            get
            {
                ISessionFactory sessionFactory = null;        

                foreach (var item in Current.SessionFactories.ToList()) {
                    foreach(var domain in item.Value.DnsRecords) {
                        if (domain.Equals(string.Empty.GetDomain())) {
                            sessionFactory = item.Value.SessionFactory;
                            break;
                        }
                    }
                }
                return sessionFactory;
            }
        }



        /// <summary>
        /// Add a SessionFactory named by a Key value,
        /// Associates a ISessionFactory with multiple domains.
        /// </summary>
        /// <param name="key">The key for this connection</param>
        /// <param name="sessionFactory">ISessionFactory to add</param>
        /// <param name="records">A list of dnsrecords(Host header values)</param>
        /// <returns>Current SessionFactoryContainer</returns>
        public SessionFactoryHostContainer Add(string key, ISessionFactory sessionFactory, List<string> records)
        {
            lock (Lock)
            {
                if (Current.SessionFactories.ContainsKey(key).Equals(false)) { 
                    Current.SessionFactories.Add(key, 
                        new SessionFactoryData{ 
                            Key = key, 
                            SessionFactory = sessionFactory, 
                            DnsRecords = records});
                }
                return Current;
            }
        }




        /// <summary>
        /// Instantiate a new Dictionary object.
        /// </summary>
        private SessionFactoryHostContainer()
        {
            SessionFactories = new Dictionary<string, SessionFactoryData>();
        }




        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() { }
    }
}
