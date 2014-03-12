using Boot.ModelFactory;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Boot.Mvc.Html
{
    public static class HtmlHelperExtensions
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        ///  Create a Zone element.
        /// </summary>
        /// <param name="html">HtmlHelper html</param>
        /// <param name="model">dunamic model</param>
        /// <param name="contentId">Content Id</param>
        /// <param name="pageId">Page Id</param>
        /// <returns>A rendered Mvc object</returns>
        public static IHtmlString Zone(this HtmlHelper html, dynamic model, Region zone)
        {
            //Single output. In normal we iterate all widgets. Just for testing.
            try { 
            var sb = new StringBuilder();
            var datamodel = (Models.PageViewModel)model;

            if (datamodel.Contents.Any()) {
                Content contents = datamodel.Contents.Where(d => d.Zone == zone && d.PageId==1).Single();

                if (zone.ToString() == contents.Zone.ToString()) {
                    sb.Append(RenderWidget(html, "~/Views/Widgets/Content.cshtml", contents));
                }
                if (sb.ToString().Length > 0)
                    return new HtmlString(sb.ToString());

                }
            }catch(Exception ex){
                log.Debug(ex.Message);
            }

            return new HtmlString("&nbsp;");
        }


        /// <summary>
        /// Renders a IWidget
        /// </summary>
        /// <typeparam name="T">Model (T) Usually a Widget</typeparam>
        /// <param name="html"></param>
        /// <param name="viewPath"></param>
        /// <param name="model">IWidget model</param>
        /// <returns>Rendered IWidget</returns>
        public static string RenderWidget<T>(this HtmlHelper html, string viewPath, T model)
        {
            try
            {
                using (var writer = new StringWriter())
                {
                    var view = new WebFormView(html.ViewContext.Controller.ControllerContext, viewPath);
                    var vdd = new ViewDataDictionary<T>(model);
                    var context = new ViewContext(html.ViewContext.Controller.ControllerContext, view, vdd, new TempDataDictionary(), writer);
                    var v = new RazorView(html.ViewContext.Controller.ControllerContext, viewPath, null, false, null);
                    v.Render(context, writer);

                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}