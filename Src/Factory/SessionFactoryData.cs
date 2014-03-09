using System.Collections.Generic;
using System.Linq;
using Boot.Multitenancy.Extensions;
using FluentNHibernate.Conventions;
using NHibernate;
using System;

namespace Boot.Multitenancy
{

    /// <summary>
    /// Holder of ISessionFactory and the associated domains.
    /// </summary>
    internal class SessionFactoryData : IDisposable
    {


        /// <summary>
        /// The key, usually the database name.
        /// </summary>
        public string Key { get; set; }




        /// <summary>
        /// List of host header values.
        /// </summary>
        public List<string> DnsRecords { get; set; }




        /// <summary>
        /// SessionFactory for this session.
        /// </summary>
        public ISessionFactory SessionFactory { get; set; }





        /// <summary>
        /// Init a new SessionFactoryData Object.
        /// </summary>
        public SessionFactoryData() { 
            DnsRecords = new List<string>(); 
        }
        



        public void Dispose() { }
    }
}
