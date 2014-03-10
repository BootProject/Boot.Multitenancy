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
            var s = new Settings { Id = 1, Title = "Boot Project", FooterText = "(c) All rights reserved Boot Project" };
            TransactionSave<Settings>(s);

            var page = new Page() { Id = 1, Action = "Index", Controller = "Home", Active = true, MetaTitle = "Boot Project", ParentId = 0, Title = "Start", Url = "" };
            TransactionSave<Page>(page);
        }

       

        /// <summary>
        /// Demonstrates the use of With<T>.
        /// In this case it uses a real transaction to perform the Save extension.
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