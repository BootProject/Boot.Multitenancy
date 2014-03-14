using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Boot.Multitenancy.Intrastructure;
using Boot.Multitenancy.Extensions;
using Boot.Multitenancy.Intrastructure.Domain;

namespace Boot.Multitenancy.Factory
{
    /// <summary>
    /// Tenant
    /// </summary>
    public class Tenant : ITenant
    {

        /// <summary>
        /// The Configuration for this Tenant
        /// </summary>
        public ITenantConfiguration Configuration { get; set; }

        //Ctor can only be created internal.
        internal Tenant() { }

        /// <summary>
        /// Init a new Tenant with ITenantConfiguration.
        /// </summary>
        /// <param name="configuration">ITenantConfiguration configuration</param>
        public Tenant(ITenantConfiguration configuration)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// Creates the ISessionFactory configuration.
        /// </summary>
        /// <returns></returns>
        public ISessionFactory CreateConfig()
        {
            return Fluently
                .Configure()
                .Database(DatabaseConfiguration)
                .Mappings(MapAssemblies)
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }


        /// <summary>
        /// Searches all assemblies for IEntity's
        /// </summary>
        /// <param name="fmc"></param>
        internal void MapAssemblies(MappingConfiguration fmc)
        {
            (from a in AppDomain.CurrentDomain.GetAssemblies()
             select a
                 into assemblies
                    select assemblies) 
                        .ToList()
                            .ForEach(a => {
                                fmc.AutoMappings.Add(AutoMap.Assembly(a)
                                    .Conventions.Add<StringColumnLengthConvention>()
                                        .Where(IsEntity));
                });
        }


        /// <summary>
        /// Creates the configuration for FluentnHibernate
        /// </summary>
        /// <returns>IPersistenceConfigurer config</returns>
        internal IPersistenceConfigurer DatabaseConfiguration()
        {
            switch (Configuration.DbType)
            {
                case DbType.MySql5:
                    return MySQLConfiguration.Standard
                            .UseOuterJoin()
                            .ConnectionString(Configuration.Connectionstring)
                            .ShowSql();
                case DbType.SqlCe:
                    return MsSqlCeConfiguration.MsSqlCe40
                            .UseOuterJoin()
                            .ConnectionString(Configuration.Connectionstring)
                            .ShowSql();
                case DbType.SqlServer2008:
                    return MsSqlConfiguration.MsSql2008
                            .UseOuterJoin()
                            .ConnectionString(Configuration.Connectionstring)
                            .ShowSql();
                default:
                    return null;
            }
        }


        /// <summary>
        /// Builds the configuration for FluentnHibernate/nHibernate
        /// </summary>
        /// <param name="config"></param>
        internal void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            SchemaMetadataUpdater.QuoteTableAndColumns(config);
            string path = string.Empty.GetConfigFile();

            new SchemaExport(config).SetDelimiter(";").SetOutputFile(path).Create(true, false);
            new SchemaUpdate(config).Execute(false, true);
        }


        /// <summary>
        /// The Entity to look for
        /// </summary>
        /// <param name="t">The type to compare</param>
        /// <returns>If type is Entity</returns>
        private static bool IsEntity(Type t)
        {
            return typeof(IEntity).IsAssignableFrom(t);
        }
    }
}
