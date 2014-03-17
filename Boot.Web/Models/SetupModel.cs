using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Boot.Multitenancy.SessionManager;
using Boot.Multitenancy.Extensions;
using Boot.ModelFactory;
using NHibernate;
using Boot.Multitenancy.Intrastructure.Domain;
using Boot.Multitenancy.Intrastructure;
using FluentNHibernate.Automapping;
using System.Reflection;
using FluentNHibernate.Cfg;
using Boot.Multitenancy.Configuration;


namespace Boot.Web.Models
{

    /// <summary>
    /// Model separation among domains.
    /// This model ensure that only object that should be installed to this domain are added.
    /// </summary>
    public class SetupModel
    {
        ISession Session { get; set; }
        public List<Type> Types { get; set; }


        public SetupModel()
        {
            Session = SessionFactoryHostContainer.CurrentFactory.OpenSession();
            GetInstalledTypes();
        }


        /// <summary>
        /// Grab all installed types by Fluent
        /// </summary>
        /// <returns>A list of name of the types.</returns>
        public void GetInstalledTypes()
        {
            Types = new List<Type>();

            foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                var assembly = Assembly.Load(assemblyName);
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass && (type.GetInterface("IEntity") != null) && (!type.IsAbstract)) { 
                        Types.Add(type);
                    }
                }
            }
        }


        private static bool IsEntity(Type t)
        {
            return typeof(IEntity).IsAssignableFrom(t);
        }


        /// <summary>
        /// Setup some default pages and contents.
        /// </summary>
        public void CreateIfNotExist()
        {
            using (var session = SessionHostFactory.With<Page>().OpenSession()) 
            { 
                if (session.QueryOver<Page>().RowCount() == 0)
                {
                    var page = new Page() { Id = 1, Action = "Index", Controller = "Home", Active = true, MetaTitle = "Boot Project", ParentId = 0, Title = "Start", Url = "" };
                    TransactionSave<Page>(page);

                    page = new Page() { Id = 2, Action = "About", Controller = "Home", Active = true, MetaTitle = "Boot Project", ParentId = 0, Title = "About", Url = "" };
                    TransactionSave<Page>(page);

                    page = new Page() { Id = 3, Action = "Contact", Controller = "Home", Active = true, MetaTitle = "Boot Project", ParentId = 0, Title = "Contact", Url = "" };
                    TransactionSave<Page>(page);

                    var settings = new Settings { Id = 1, Title = "Boot Project", FooterText = "(c) All rights reserved Boot Project " + string.Empty.GetDomain() };
                    TransactionSave<Settings>(settings);
                }
            }

            using (var session = SessionHostFactory.With<Content>().OpenSession())
            {
                if (session.QueryOver<Content>().RowCount() == 0)
                {
                    var content1 = new Content { Id = 1, PageId = 1, Zone = Region.Header, Title = "<h1>Boot Multitenancy</h1>", Html = "<p class='lead'>Read our guidelines at our development website at bitbucket. There's a lot of valueable information when using this project in your application. <br/><strong>All text on this page is generated from your database.</strong></p>" };
                    var content2 = new Content { Id = 2, PageId = 1, Zone = Region.TripelFirst, Title = "<h2>Getting started</h2>", Html = "<p>Visit Bitbucket to learn how to get started, and read information about this and other project in this series.</p><br/>" };
                    var content3 = new Content { Id = 3, PageId = 1, Zone = Region.TripelSecond, Title = "<h2>Get more libraries</h2>", Html = "<p>Visit the developers site where you can download several project.</p><br><br/>" };
                    var content4 = new Content { Id = 4, PageId = 1, Zone = Region.TripelThird, Title = "<h2>About Boot Project</h2>", Html = "<p>Boot Project is a project for developing and enhance content management systems. Project are built as their own solutions and can be deployed into any application.</p>" };

                    var content5 = new Content { Id = 5, PageId = 1, Zone = Region.AsideFirst, Title = "<h2>Multitenancy</h2>", Html = "<p>This project is an extension of nHibernate's ISessionFactory and makes it incredible easy to use multiple sessions within the same application. In BootCms it's used to define different domains and their databases. Read more There are several way of completing configuration, from code, by Host and Tenants or by web.config. Boot.Multitenancy has Fluentnhibernate built in, so no need for extra configuration except setting up databases configuration i web.config. Boot.Multitenany = By dropping in 2 lines of code and add configuration to web.config</p><blockquote class=\"blockquote-reverse\"><p>Boot.Multitenancy</p>Boot.Multitenancy configuration has it's setup in web.config. You can assign multiple domains to connect to a database. To add a database and domains, see configuration below.</blockquote>" };
                    var content6 = new Content { Id = 6, PageId = 1, Zone = Region.AsideSecond, Title = "<blockquote class=\"blockquote\"><h2>HostContainer <span class=\"label label-default\">New</span></h2>", Html = "If your \"lazy\" like I am, and want a quick and easy way of start using Multitenancy, add some configuration to web.config and you're up and running. All Fluentnhibernate configuration is built in. A default host and tenants are automatically configured. Your application can be setup to create configuration from web.config <a href=\"https://bitbucket.org/rickardmagnusson/boot.multitenancy/wiki/SessionFactoryHostContainer\" target=\"_blank\">Read this guide.</a></blockquote>" };

                    var content7 = new Content { Id = 7, PageId = 2, Zone = Region.Content, Title = "<h1>About Boot Project</h1>", Html = "<p>This project is an extension of nHibernate's ISessionFactory and makes it incredible easy to use multiple sessions within the same application. In BootCms it's used to define different domains and their databases. Read more There are several way of completing configuration, from code, by Host and Tenants or by web.config. Boot.Multitenancy has Fluentnhibernate built in, so no need for extra configuration except setting up databases configuration i web.config. Boot.Multitenany = By dropping in 2 lines of code and add configuration to web.config</p><blockquote class=\"blockquote-reverse\"><p>Boot.Multitenancy</p>Boot.Multitenancy configuration has it's setup in web.config. You can assign multiple domains to connect to a database. To add a database and domains, see configuration below.</blockquote>" };
                    var content8 = new Content { Id = 8, PageId = 3, Zone = Region.Content, Title = "<h1>Contact</h1>", Html = "<a href=\"https://bitbucket.org/rickardmagnusson/\" target=\"_blank\">Contact us at Bitbucket.</a>" };

                    session.WithTransaction(s => { s.Save(content1); });
                    session.WithTransaction(s => { s.Save(content2); });
                    session.WithTransaction(s => { s.Save(content3); });
                    session.WithTransaction(s => { s.Save(content4); });
                    session.WithTransaction(s => { s.Save(content5); });
                    session.WithTransaction(s => { s.Save(content6); });
                    session.WithTransaction(s => { s.Save(content7); });
                    session.WithTransaction(s => { s.Save(content8); });
                }
            }
        }


        /// <summary>
        /// Demonstrates the use of With<T>.
        /// In this case it uses a real transaction to perform the Save 
        /// A wrapper for save with transaction.
        /// Inserts a new object if theres no object created before.
        /// </summary>
        /// <typeparam name="T">Type to save</typeparam>
        /// <param name="t">The new object to save</param>
        public void TransactionSave<T>(T t) where T : class
        {
            using (var session = SessionHostFactory.With<T>().OpenSession())
            {
                session.WithTransaction(s => { s.Save(t); });
            }
        }
    }
}