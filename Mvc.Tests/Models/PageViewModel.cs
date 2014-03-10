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

        public ISession Conn { get; set; }

        public PageViewModel()
        {
            Conn = SessionFactoryHostContainer.CurrentFactory.OpenSession();
            
            InsertPage();
            CreateSettings();

            Settings = Conn.Get<Settings>(1);
            Page = Conn.Get<Page>(1);

            //this.log.Debug("Created settings");
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
                return Conn.All<Page>();
            }
        }

        public void GetEl()
        {
            using (var session = SessionFactory.With<Page>().OpenSession())
            {
                session.BeginTransaction();
            }
        }

        public void CreateSettings()
        {
            if (Conn.QueryOver<Settings>().RowCount() == 0)
            {
                var s = new Settings { Id = 1, Title = "Boot Project", FooterText = "(c) All rights reserved Boot Project" };
                Conn.Save(s);
                Conn.Flush();
            }
        }

        //Create first page
        public void InsertPage()
        {
            if (Conn.QueryOver<Page>().RowCount() == 0)
            {
                Page p = new Page()
                {
                    Id = 1,
                    Action = "Index",
                    Controller = "Home",
                    Active = true,
                    MetaTitle = "Boot Project",
                    ParentId = 0,
                    Title = "Start",
                    Url = ""
                };
                Conn.Save<Page>(p);
                Conn.Flush();
            } 
        }
    }
}