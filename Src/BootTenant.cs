﻿using Boot.Multitenancy.Filters;
using Boot.Multitenancy.Filters.Extensions;
using Boot.Multitenancy.Infrastructure;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy
{
    public class BootTenant : ISessionFactoryCreator
    {

        private static string Connectionstring { get; set; }


        /// <summary>
        /// Init a new Tenant with a connectionstring.
        /// </summary>
        /// <param name="connectionstring"></param>
        public BootTenant(string connectionstring)
        {
            Connectionstring = connectionstring;
        }



        /// <summary>
        /// Init and return a SessionFactory.
        /// </summary>
        /// <returns></returns>
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
        ///     Maps all Entities in bin folder.
        /// </summary>
        /// <param name="fmc">MappingConfiguration mapping</param>
        private static void MapAssemblies(MappingConfiguration fmc)
        {
            (from a in AppDomain.CurrentDomain.GetAssemblies()
             select a
                 into assemblies
                 select assemblies)
                .ToList()
                .ForEach(a =>
                {   //Ignore other assemblies since there's no Entity's created in them. (MSCoreLib makes load fail)
                    if (a.FullName.StartsWith("Boot"))
                    {
                        fmc.AutoMappings.Add(AutoMap.Assembly(a)
                            .OverrideAll(p => p.SkipProperty(typeof(NoProperty)))
                            .Where(IsEntity));
                    }
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

            //TODO: Select custom provider
            return MsSqlCeConfiguration.MsSqlCe40
                .UseOuterJoin()
                .ConnectionString(Connectionstring)
                .ShowSql();
        }




        /// <summary>
        ///     Validates nHibernate schema
        /// </summary>
        /// <param name="config">Configuration config</param>
        private static void ValidateSchema(NHibernate.Cfg.Configuration config)
        {
            var check = new SchemaValidator(config);
        }





        /// <summary>
        ///     Creates or build the current database.
        /// </summary>
        /// <param name="config"></param>
        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            SchemaMetadataUpdater.QuoteTableAndColumns(config);
            new SchemaUpdate(config).Execute(false, true);
     
        }

    }
}