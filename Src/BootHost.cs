using Boot.Multitenancy.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conf = Boot.Multitenancy;

namespace Boot.Multitenancy
{
    public static class BootHost
    {
        public static void Init()
        {
            if (CreateEnvironment())
                Setup();
        }

        public static void Setup()
        {
            (from c in ConvertToList(Collection)
                select c).ToList().ForEach(d => {
                    new BootTenant(Conf.Configuration.ConnectionstringConfiguration.CreateConnectionstring(DbType.SqlCe, d.Name));
                });
        }


        public static List<DatabaseSection> ConvertToList(ICollection col)
        {
            return Collection.CollectionToList<DatabaseSection>();
        }

        public static DatabaseCollection Collection
        {
            get { return Configuration.Databases; }
        }


        public static bool CreateEnvironment()
        {
            return Configuration.Persist;
        }


        public static SessionFactoryConfiguration Configuration
        {
            get { return Conf.Configuration.DatabaseCollectionReader.conf; }
        }
    }


    public static class Helpers
    {
        public static List<T> CollectionToList<T>(this System.Collections.ICollection other)
        {
            var output = new List<T>(other.Count);

            output.AddRange(other.Cast<T>());

            return output;
        }
    }
}
