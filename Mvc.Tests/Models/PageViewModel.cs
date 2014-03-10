using Boot.ModelFactory;
using Boot.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using Boot.Multitenancy.Extensions;
using log4net;
using System.Reflection;
using FluentNHibernate.Mapping;
using Boot.Multitenancy.Infrastructure;

namespace WebApplication1.Models
{
    //testmodel
    public class PageViewModel
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ISession Session { get; set; }

        public PageViewModel()
        {
            Session = SessionFactoryHostContainer.CurrentFactory.OpenSession();

            CreateDefaults();
            Settings = Session.Get<Settings>(1);
            Page = Session.Get<Page>(1);

        }


        public Page Page
        {
            get;
            set;
        }

        public Settings Settings
        {
            get;
            set;
        }

        public List<Page> Pages
        {
            get
            {
                return Session.All<Page>();
            }
        }


        /// <summary>
        /// Create some default in database. 
        /// </summary>
        public void CreateDefaults()
        {
            try {
                CreateContent();

                var page = new Page() { Id = 1, Action = "Index", Controller = "Home", Active = true, MetaTitle = "Boot Project", ParentId = 0, Title = "Start", Url = "" };
                TransactionSave<Page>(page);

                var s = new Settings { Id = 1, Title = "Boot Project", FooterText = "(c) All rights reserved Boot Project " + string.Empty.GetDomain()  };
                TransactionSave<Settings>(s);
            }
            catch (Exception ex)
            {
                log.Debug(ex.InnerException);
            }
        }



        public Content GetContent(Int32 id, Int32 pageId)
        {
            return Contents.SingleOrDefault(c => c.Id == id && c.PageId==pageId);
        }



        public List<Content> Contents
        {
            get { return Session.All<Content>(); }
        }


        public void CreateContent()
        {
            using (var session = SessionHostFactory.With<Content>().OpenSession())
            {
                if (session.QueryOver<Content>().RowCount() == 0)
                {
                    var content1 = new Content { Id = 1, PageId = 1, Title = "Boot Multitenancy", Html = "<p class='lead'>Read our guidelines at our development website at bitbucket. There's a lot of valueable information when using this project in your application.</p>" };
                    var content2 = new Content { Id = 2, PageId = 1, Title = "Getting started", Html = "<p>Visit Bitbucket to learn how to get started, and read information about this and other project in this series.</p>" };
                    var content3 = new Content { Id = 3, PageId = 1, Title = "Get more libraries", Html = "<p>Visit the developers site where you can download several project.</p>" };
                    var content4 = new Content { Id = 4, PageId = 1, Title = "About Boot Project", Html = "<p>Boot Project is a project for developing and enhance content management systems. Project are built as their own solutions and can be deployed into any application.</p>" };

                    session.WithTransaction(s => { s.Save(content1); });
                    session.WithTransaction(s => { s.Save(content2); });
                    session.WithTransaction(s => { s.Save(content3); });
                    session.WithTransaction(s => { s.Save(content4); });
                    session.Flush();
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
                if (session.QueryOver<T>().RowCount() == 0) {
                    session.WithTransaction(s => { s.Save(t); });
                }
            }
        }
    }
}