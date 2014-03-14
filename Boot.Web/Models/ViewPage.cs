using Boot.Web.Mvc.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Boot.Web.Models
{
    /// <summary>
    /// Base class for all pages. Contain a lot of HtmlExtension helpers.
    /// </summary>
    /// <typeparam name="TModel">A dynamic model as strongly typed class</typeparam>
    public abstract class ViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private static ViewContext context;
        private static IViewDataContainer container;
        private readonly HtmlHelperFactory factory = new HtmlHelperFactory();
        public static HtmlHelper<TModel> Html { get; private set; }

        private void CreateHelper(TModel model)
        {
            Html = factory.CreateHtmlHelper(model, new StringWriter(new StringBuilder()));
        }

        internal virtual TModel BaseModel
        {
            get { return base.Model; }
            set { CreateHelper(value); }
        }

        /// <summary>
        /// Initialize all helpers
        /// </summary>
        public override void InitHelpers()
        {
            base.InitHelpers();
            context = ViewContext;
            container = this;
            Html = new HtmlHelper<TModel>(context, container);
        }

        #region Helpers

        public static dynamic Zone(Region zone)
        {
            dynamic model = Html.ViewDataContainer.ViewData.Model;
            return HtmlHelperExtensions.Zone(Html, model, zone);
        }


        #endregion
    }
}