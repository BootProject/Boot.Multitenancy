using Boot.Multitenancy.Intrastructure.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.ModelFactory.Infrastructure.Widgets
{
    /// <summary>
    /// Default base class for widgets.
    /// Every class that inherits Widget, automaticall get registered as a 
    /// widget and can be used in Zones. <see href="Zone">Zone element</see>.
    /// </summary>
    /// <typeparam name="T">The type to persist</typeparam>
    public abstract class Widget<T> : ClassMap<T>, IWidget where T : class
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Int32 Id { get; set; }


        /// <summary>
        /// Where to place this widget
        /// </summary>
        public virtual Region Zone { get; set; }


        /// <summary>
        /// The page id for this widget. Used for placement.
        /// </summary>
        public virtual Int32 PageId { get; set; }

        /// <summary>
        /// The path to the .cshtml file. Located in Views/Widget folder.
        /// </summary>
        public virtual string Template()
        {
            return string.Format("~/Views/Widgets/{0}.cshtml", this.GetType().Name);
        }

        public Widget()
        {
           
        }
    }

    /*
    //Implementation. Note not implemented yet!!
    public class ContentWidget
    {
        public virtual string Html { get; set; }
    }

    public class ContentWidgetMap : Widget<ContentWidget>
    {
        public ContentWidgetMap()
        {
            Map(p => p.Html);
        }
    }*/
    
     
}
