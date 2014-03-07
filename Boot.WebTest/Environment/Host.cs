using System;
using System.IO;
using System.Linq;
using Boot.Multitenancy;
using Boot.Multitenancy.Extensions;
using FluentNHibernate.Conventions;
using Boot.Multitenancy.Configuration;

namespace Boot.WebTest.Environment
{
    public static class Host
    {
        public static void Init()
        {
            //Create 2 test databases.
            SessionFactoryContainer.Current
                .Add("First", new Tenant("Data Source=|DataDirectory|FirstDB.sdf;Persist Security Info=False;").Create())
                .Add("Second", new Tenant("Data Source=|DataDirectory|SecondDB.sdf;Persist Security Info=False;").Create());

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