using Boot.ModelFactory;
using Boot.Multitenancy.SessionManager;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Boot.Web.Mvc.Html;
using Boot.Multitenancy.Extensions;

namespace Boot.Web.Models
{

    /// <summary>
    /// Default PageViewModel
    /// </summary>
    public class PageViewModel
    {

        /// <summary>
        /// The Page Title 
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// The current nHibernate ISession
        /// </summary>
        public ISession Session { get; set; }


        public string Theme { get { return SessionFactoryHostContainer.Current.Theme(); } }

        /// <summary>
        /// Ctor
        /// </summary>
        public PageViewModel()
        {
            Session = SessionFactoryHostContainer.CurrentFactory.OpenSession();
        }


        /// <summary>
        /// Get current Page
        /// </summary>
        public Page Page
        {
            get
            {
                var page = Pages.Single<Page>(p => p.Id == Page.Current(this).Id);
                Title = page.Title;
                return page;
            }
        }


        /// <summary>
        /// All list of all Pages for this domain
        /// </summary>
        public List<Page> Pages
        {
            get { return Session.All<Page>(); }
        }


        /// <summary>
        /// Settings for this domain
        /// </summary>
        public Settings Settings
        {
            get { return Session.Get<Settings>(1); }
        }


        /// <summary>
        /// A list of content for this domain
        /// </summary>
        public List<Content> Contents
        {
            get { return Session.All<Content>(); }
        }
    }
}