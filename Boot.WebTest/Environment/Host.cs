using System;
using System.IO;
using System.Linq;
using Boot.Multitenancy;
using Boot.Multitenancy.Extensions;
using FluentNHibernate.Conventions;
using Boot.Multitenancy.Configuration;
using Con = Boot.Multitenancy.Configuration.ConnectionstringConfiguration;
using Conf = Boot.Multitenancy;

namespace Boot.WebTest.Environment
{
    public static class Host
    {
        public static void Init()
        {
            bool persist = Conf.Configuration.DatabaseCollectionReader.conf.Persist;

            //Create 2 test databases.
            SessionFactoryContainer.Current
                .Add("First", new Tenant(Con.CreateConnectionstring(DbType.SqlCe, "First")).Create())
                .Add("Second", new Tenant(Con.CreateConnectionstring(DbType.SqlCe, "Second")).Create());

            CheckEnvironmentSetup();
        }

        private static void CheckEnvironmentSetup()
        {
            if (SessionFactory.With<ModelTest>().IsNotAny()) { 

                SessionFactory
                    .With<ModelTest>("First") //Open connection first
                    .OpenSession()
                    //Create 3 new ModelTest items in db...
                    .Save<ModelTest>(new ModelTest
                    {
                        Id = 1,
                        Text = "First Item.)"
                    })
                    .Save<ModelTest>(new ModelTest
                    {
                        Id = 2,
                        Text = "BootProject Footer"
                    })
                    .Save<ModelTest>(new ModelTest
                    {
                        Id = 3,
                        Text = "Welcome to BootProject."
                    });

                SessionFactory
                    .With<ModelTest>("Second") //Open connection second
                    .OpenSession()
                    .Save<ModelTest>(new ModelTest
                    {
                        Id = 1,
                        Text = "This is the result from Second db."
                    });
            }

        }
    }
}