using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Boot.Multitenancy.Configuration
{
    public class DatabaseCollectionReader
    {
        static SessionFactoryConfiguration conf { get; set; }

        static DatabaseCollectionReader()
        {
            var conf = ConfigurationManager.GetSection("Databases") as SessionFactoryConfiguration;
            var db = conf.Databases;
        }
    }
}
