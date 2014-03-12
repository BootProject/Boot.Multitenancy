using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy
{
    public interface ITenant
    {
        string Key { get; set; }
        SessionFactory SessionFactory { get; set; }
    }


    /*
      public class Tenant{
        
        public BootTenant(string dbname, DbType dbtype, string connectionstring, List<string> domains, SessionFactoryHostContainer creator)
        {
            
        }
      }
     * 
     * 
     * 
     * 
     * 
     *  /// <summary>
        ///  Creates instance of databases.
        /// </summary>
        private static void InitConfiguration()
        {
            var session = SessionFactoryHostContainer.Current;

            (from configuration in Databases
             select configuration)
                    .ToList()
                        .ForEach(database =>
                        {
                            session.Add
                            (
                                new BootTenant
                                (
                                    database.Name, 
                                    database.DbType,
                                    Con.CreateConnectionstring(database.DbType, database.Name),
                                    database.DomainList,
                                    session 
                                );
                        }
            );
        }
     * 
     * 
     * 
     * 
     * 
        internal Dictionary<string, ITenant> Tenants { get; set; }
        public SessionFactoryHostContainer Add(ITenant tenant)
        {
            lock (Lock)
            {
                tenant.ISessionFactory = tenant.Create();
                SessionFactoriesTenants.Add(tenant.Key, tenant);
                return Current;
            }
        }

     */
}
