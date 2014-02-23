
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
        /// Get current SessionFactory.
        /// </summary>
        /// <typeparam name="T">The type to get ISession for</typeparam>
        /// <returns>ISessionFactory</returns>
        public static ISessionFactory For<T>()
        {
            return SessionFactoryContainer.Current.SessionFactories
                .ToList()
                    .Find(d => d.Key.Equals(string.Empty.Key()))
                        .Value;
        }
    }
}
