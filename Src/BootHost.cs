using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Boot.Multitenancy.Extensions;
using Boot.Multitenancy.Configuration;
using Conf = Boot.Multitenancy;
using Con = Boot.Multitenancy.Configuration.ConnectionstringConfiguration;

namespace Boot.Multitenancy
{

    /// <summary>
    /// Static implementation of BootHost.
    /// Inits configuration and create install scripts for database.
    /// </summary>
    public static class BootHost
    {


        /// <summary>
        /// A lazy init of Boot.Multitenancy
        /// Reads out information from web.config and create items depending on configuration.
        /// </summary>
        public static void Init()
        {
            if (CreateEnvironment())
                Setup();
        }



        /// <summary>
        /// Reads out configuration before creating scripts.
        /// Usually used to create database before init configuration.
        /// When ready, Call Init();
        /// </summary>
        /// <returns>A list of ConnectionElement</returns>
        public static List<ConnectionElement> InitCreate()
        {
            var connectionElements = new List<ConnectionElement>();
            (from d in Databases select d).ToList().ForEach(p => {
                connectionElements.Add(
                    new ConnectionElement { 
                        Name = p.Name, 
                        Connectionstring = Con.CreateConnectionstring(p.DbType, p.Name) 
                    });
            });

            return connectionElements;
        }



        /// <summary>
        /// Creates instance of databases.
        /// </summary>
        private static void Setup()
        {
            using (var session = SessionFactoryContainer.Current) {
                (from configuration in Databases
                    select configuration)
                        .ToList()
                            .ForEach(database => {
                                session.Add(database.Name, 
                                    new BootTenant(Con.CreateConnectionstring(database.DbType, database.Name))
                                      .Create());
                             }
                        
                );
            }
        }



        /// <summary>
        /// Varaiable to check for setup.
        /// </summary>
        /// <returns></returns>
        private static bool CreateEnvironment()
        {
            return Configuration.Persist;
        }



        /// <summary>
        /// A set of databases listed from web.config
        /// </summary>
        private static List<DatabaseSection> Databases
        {
            get { return ConvertToList(Collection); }
        }



        /// <summary>
        /// Convert a Collection to a List<T>
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        private static List<DatabaseSection> ConvertToList(ICollection col)
        {
            return Collection.CollectionToList<DatabaseSection>();
        }



        /// <summary>
        /// The DatabaseCollection. 
        /// </summary>
        private static DatabaseCollection Collection
        {
            get { return Configuration.Databases; }
        }



        /// <summary>
        /// Get the SessionFactoryConfiguration
        /// </summary>
        private static SessionFactoryConfiguration Configuration
        {
            get { return Conf.Configuration.DatabaseCollectionReader.conf; }
        }
    }

}
