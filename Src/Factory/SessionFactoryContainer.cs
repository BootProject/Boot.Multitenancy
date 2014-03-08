
using System.Collections.Generic;
using System.Linq;
using Boot.Multitenancy.Extensions;
using FluentNHibernate.Conventions;
using NHibernate;
using System;

namespace Boot.Multitenancy
{

    /// <summary>
    /// Creates SessionFactories
    /// </summary>
    public class SessionFactoryContainer : IDisposable
    {
        
        /// <summary>
        /// Current ISessionFactory
        /// </summary>
        public static SessionFactoryContainer Current { get; private set; }



        //Internals
        private static readonly object Lock = new object();
        internal Dictionary<string, ISessionFactory> SessionFactories { get; set; }



        //Init
        static SessionFactoryContainer()
        {
            lock (Lock) {
                Current = new SessionFactoryContainer();
            }
        }



        /// <summary>
        /// Get the Current ISessionFactory
        /// </summary>
        public static ISessionFactory CurrentFactory
        {
            get
            {
                return Current.SessionFactories
                    .ToList()
                      .Find(d => d.Key.Equals(string.Empty.Key()))
                        .Value;
            }
        }


        /// <summary>
        /// Get CurrentFactory by named Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ISessionFactory GetCurrentFactory(string key)
        {
            return Current.SessionFactories
                .ToList()
                  .Find(d => d.Key.Equals(key))
                    .Value;
        }



        /// <summary>
        /// Add a SessionFactory named by a Key value
        /// </summary>
        /// <param name="key">The key for this connection</param>
        /// <param name="sessionFactory">ISessionFactory to add</param>
        /// <returns>Current SessionFactoryContainer</returns>
        public SessionFactoryContainer Add(string key, ISessionFactory sessionFactory)
        {
            lock (Lock)
            {
                if (Current.SessionFactories.ContainsKey(key).IsNotAny())
                    Current.SessionFactories.Add(key, sessionFactory);
                return Current;
            }
        }



        /// <summary>
        /// Instantiate a new Dictionary object.
        /// </summary>
        private SessionFactoryContainer()
        {
            SessionFactories = new Dictionary<string, ISessionFactory>();
        }


        public void Dispose() { }
    }
}
