using Boot.ModelFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Boot.Web.Mvc.Html
{
    public static class HtmlHelperExtensions
    {

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
            try
            {
                var sb = new StringBuilder();
                var viewmodel = (Models.PageViewModel)model;
                var current = Current(model);
                var contents = viewmodel.Contents.FindAll(d => d.PageId == current.Id);

                foreach (var item in contents) //MySql saves enum as string... strange!!
                {
                    if (item.Zone.ToString() == zone.ToString())
                    {
                        sb.Append(RenderWidget(html, "~/Views/Widgets/Content.cshtml", item));
                    }
                }

                if (sb.ToString().Length > 0)
                    return new HtmlString(sb.ToString());


            }
            catch (Exception ex)
            {
               
            }

            return new HtmlString("&nbsp;");
        }

        public static Page Current(dynamic model)
        {
            var viewmodel = (Models.PageViewModel)model;
            return viewmodel.Pages.Find(p => p.Action == string.Empty.Action() && p.Controller == string.Empty.Controller());
        }


        public static Page Current(this Page page, dynamic model)
        {
            var viewmodel = (Models.PageViewModel)model;
            return viewmodel.Pages.Find(p => p.Action == string.Empty.Action() && p.Controller == string.Empty.Controller());
        }

        public static string Controller(this string s)
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
        }

        public static string Action(this string s)
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
        }

        public static string HomeController(this string s)
        {
            return "Home";
        }

        public static string HomeAction(this string s)
        {
            return "Index";
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