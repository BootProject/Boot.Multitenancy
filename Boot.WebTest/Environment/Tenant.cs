
using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using Boot.Multitenancy;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace Boot.WebTest.Environment
{
    /// <summary>
    /// Tenant example.
    /// </summary>
    public class Tenant : ISessionFactoryCreator
    {

        public string Connectionstring { get; private set; }


        public Tenant(string connectionstring)
        {
            Connectionstring = connectionstring;
        }


        public NHibernate.ISessionFactory Create()
        {
            return Fluently.Configure()
                    .Database(MsSqlCeConfiguration
                        .MsSqlCe40
                        .ShowSql()
                        .FormatSql()
                        .ConnectionString(Connectionstring))
                    .Mappings(x => x.FluentMappings.Add(typeof(ModelTestMap)))
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
        }

        private void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            try{
                SchemaMetadataUpdater.QuoteTableAndColumns(config);
                new SchemaUpdate(config).Execute(false, true);
            }
            catch //BELOW FOR TEST ONLY!!
            {
                new SqlCeEngine { LocalConnectionString = Connectionstring }.CreateDatabase();

                SchemaMetadataUpdater.QuoteTableAndColumns(config);
                new SchemaUpdate(config).Execute(false, true);
            }
        }
    }
}