using Boot.Multitenancy.Configuration;
using Boot.Multitenancy.Filters;
using Boot.Multitenancy.Filters.Extensions;
using Boot.Multitenancy.Infrastructure;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Boot.Multitenancy
{
    public class BootTenant : ISessionFactoryCreator
    {

        private static string Connectionstring { get; set; }
        private static DbType DbType { get; set; }



        /// <summary>
        /// Init a new Tenant with a connectionstring.
        /// </summary>
        /// <param name="connectionstring">Database connectionstring</param>
        public BootTenant(string connectionstring, DbType dbtype)
        {
            Connectionstring = connectionstring;
            DbType = dbtype;
        }




        /// <summary>
        /// Create SessionFactory.
        /// </summary>
        /// <returns>ISessionFactory</returns>
        public NHibernate.ISessionFactory Create()
        {
            return Fluently
                  .Configure()
                  .Database(DatabaseConfiguration)
                  .Mappings(MapAssemblies)
                  .ExposeConfiguration(BuildSchema)
                  .ExposeConfiguration(ValidateSchema)
                  .BuildSessionFactory();
        }



        /// <summary>
        /// Returns the namespace within assemblies to look for IEntity.
        /// </summary>
        private static string Namespace
        {
            get {
                    var conf = WebConfigurationManager
                            .GetSection("sessionFactoryConfiguration") 
                                    as SessionFactoryConfiguration;
                    if(string.IsNullOrEmpty(conf.Namespace))
                        return "Boot";

                    return conf.Namespace;
                }
        }



        /// <summary>
        ///     Maps all Entities in bin folder.
        /// </summary>
        /// <param name="fmc">MappingConfiguration mapping</param>
        private static void MapAssemblies(MappingConfiguration fmc)
        {
            (from a in AppDomain.CurrentDomain.GetAssemblies()
             select a
                 into assemblies
                 select assemblies).Where(f => f.FullName.StartsWith("Boot"))
                .ToList()
                .ForEach(a =>
                {   //Ignore other assemblies since we haven't created any Entity's in them. (MSCoreLib makes load fail)
                    //if (a.FullName.StartsWith("Boot"))
                    //{
                        fmc.AutoMappings.Add(AutoMap.Assembly(a)
                        .OverrideAll(p => p.SkipProperty(typeof(NoProperty)))
                        .Where(IsEntity));
                    //}
                });
        }




        /// <summary>
        ///     Check when either a Type is a real Entity.
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <returns>True if Entity</returns>
        private static bool IsEntity(Type t)
        {
            return typeof(IEntity).IsAssignableFrom(t);
        }




        /// <summary>
        ///     Set database information.
        /// </summary>
        /// <returns>IPersistenceConfigurer configuration</returns>
        private static IPersistenceConfigurer DatabaseConfiguration()
        {
            switch (DbType)
            {
                case DbType.MySql5:
                    return MySQLConfiguration.Standard
                            .UseOuterJoin()
                            .ConnectionString(Connectionstring)
                            .ShowSql();
                case DbType.SqlCe:
                    return MsSqlCeConfiguration.MsSqlCe40
                            .UseOuterJoin()
                            .ConnectionString(Connectionstring)
                            .ShowSql();
                default:
                    return null;
            }
        }




        /// <summary>
        ///     Validates nHibernate schema
        /// </summary>
        /// <param name="config">NHibernate.Cfg.Configuration</param>
        private static void ValidateSchema(NHibernate.Cfg.Configuration config)
        {
            var check = new SchemaValidator(config);
        }





        /// <summary>
        ///     Creates or build the current database.
        ///     Creates a file in App_Data, FluentConfiguration.xml.
        /// </summary>
        /// <param name="config">NHibernate.Cfg.Configuration</param>
        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            SchemaMetadataUpdater.QuoteTableAndColumns(config);
            string path = AppDomain.CurrentDomain.GetData("DataDirectory") + "\\FluentConfiguration.xml";
            new SchemaExport(config)
                .SetDelimiter(";")
                .SetOutputFile(path)
                .Create(false, false);
            new SchemaUpdate(config).Execute(false, true);
        }
    }
}
