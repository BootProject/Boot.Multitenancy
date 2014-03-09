using System.Collections.Generic;
using System.Linq;
using Boot.Multitenancy.Extensions;
using FluentNHibernate.Conventions;
using NHibernate;
using System;

namespace Boot.Multitenancy
{
    internal class SessionFactoryData : IDisposable
    {
        public string Key { get; set; }
        public List<string> DnsRecords { get; set; }
        public ISessionFactory SessionFactory { get; set; }

        public SessionFactoryData() { 
            DnsRecords = new List<string>(); 
        }
        

        public ISessionFactory GetCurrent()
        {
            if (DnsRecords.Find(d => d == string.Empty.GetDomain()).IsNotAny())
                return null;
            return SessionFactory;
        }

        public void Dispose() { }
    }
}
