using System.Collections.Generic;
using System.Linq;
using Boot.Multitenancy.Extensions;
using FluentNHibernate.Conventions;
using NHibernate;
using System;
using System.Text;
using NHibernate.Proxy.DynamicProxy;
using Boot.Multitenancy.Proxy;
using System.ComponentModel;

namespace Boot.Multitenancy
{
    /// <summary>
    /// Contains methods to read multiple domains from a single SessionFactory object.
    /// </summary>
    public class SessionFactoryHostContainer //: IDisposable
    {

        /// <summary>
        /// The current SessionFactoryHostContainer.
        /// </summary>
        public static SessionFactoryHostContainer Current { get; private set; }

        public static string Theme { get; private set; }

        private static readonly object Lock = new object();
        internal Dictionary<string, SessionFactoryData> SessionFactories { get; set; }



        /// <summary>
        /// Tests
        /// </summary>
        /// <returns></returns>
        public static List<SessionFactoryData> GetAll()
        {
            List<SessionFactoryData> list = new List<SessionFactoryData>();
            foreach (var item in Current.SessionFactories.ToList())
                list.Add(item.Value);

            return list;
        }


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
                            Theme = item.Value.Theme;
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
        public SessionFactoryHostContainer Add(string key, ISessionFactory sessionFactory, List<string> records, string theme)
        {
            lock (Lock)
            {
                if (Current.SessionFactories.ContainsKey(key).Equals(false)) { 
                    Current.SessionFactories.Add(key, 
                        new SessionFactoryData{ 
                            Key = key, 
                            SessionFactory = sessionFactory, 
                            DnsRecords = records,
                            Theme = theme});
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

    }
}
