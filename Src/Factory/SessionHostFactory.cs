using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boot.Multitenancy.Extensions;

namespace Boot.Multitenancy
{
    /// <summary>
    /// Extensions for ISessionFactory
    /// </summary>
    public static class SessionHostFactory
    {

        /// <summary>
        ///  Get current SessionHostFactory.
        /// Generic T is persisted to nhibernate.
        /// </summary>
        /// <returns>ISessionFactory</returns>
        public static ISessionFactory With<T>()
        {
            return SessionFactoryHostContainer.CurrentFactory;  
        }
    }
}
