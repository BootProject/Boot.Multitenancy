using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy
{
    public class Registry 
    {
        internal static Dictionary<string, ISessionFactory> Factories { get; set; }

        static Registry()
        {
            Factories = new Dictionary<string, ISessionFactory>();
        }

        internal void Add(string key, ISessionFactory value)
        {
            Factories.Add(key, value);
        }


        public void AutoList()
        {

        }
    }
}
