
using System;
using System.Linq;
using Boot.Multitenancy.Extensions;
using NHibernate;

namespace Boot.Multitenancy
{
    /// <summary>
    /// Creates a connection to ISessionFactory
    /// </summary>
    public static class SessionFactory
    {
        

        /// <summary>
        ///  Get current SessionFactory.
        /// Used to get ISessionFactory based on a domain name. See documentation. Also see <see cref="StringExtensions.Key"/>.
        /// Generic T is persisted to nhibernate.
        /// </summary>
        /// <returns>ISessionFactory</returns>
        public static ISessionFactory With<T>()
        {
            return SessionFactoryContainer.Current.SessionFactories
                .ToList()
                    .Find(d => d.Key.Equals(string.Empty.Key()))
                        .Value;
        }



        /// <summary>
        ///  Get current SessionFactory.
        ///  Generic T is persisted to nhibernate.
        /// </summary>
        ///<param name="key">The key to return ISessionFactory from</param>
        /// <returns>ISessionFactory</returns>
        public static ISessionFactory With<T>(string key)
        {
            return SessionFactoryContainer.Current.SessionFactories
                .ToList()
                    .Find(d => d.Key.Equals(key))
                        .Value;
        }
    }
}
