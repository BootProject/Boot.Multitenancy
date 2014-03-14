using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.SessionManager
{
    /// <summary>
    /// Extensions for ISessionFactory
    /// </summary>
    public static class SessionHostFactory
    {

        /// <summary>
        ///  Get current SessionHostFactory.
        ///  Generic T is persisted to nhibernate.
        /// </summary>
        /// <returns>ISessionFactory</returns>
        public static ISessionFactory With<T>()
        {
            return SessionFactoryHostContainer.CurrentFactory;
        }
    }
}
