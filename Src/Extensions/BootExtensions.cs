using Boot.Multitenancy.SessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Boot.Multitenancy.Extensions
{
    /// <summary>
    /// Contains various helpers for Boot.Multitenancy.
    /// </summary>
    public static class BootExtensions
    {

        /// <summary>
        /// The FluentConfiguration output files path.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Path to FluentConfiguration output file</returns>
        public static string GetConfigFile(this string s)
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory") + "\\FluentConfiguration.xml";
        }


        /// <summary>
        /// Get the current domain
        /// </summary>
        /// <param name="s">String to get domain from.</param>
        /// <returns>The current doamin.</returns>
        public static string GetDomain(this string s)
        {
            return new Uri(HttpContext.Current.Request.Url.ToString()) //No need to throw exception here if not found.
                    .GetComponents(
                    UriComponents.AbsoluteUri &
                    ~UriComponents.Port &
                    ~UriComponents.Path &
                    ~UriComponents.Scheme,
                    UriFormat.UriEscaped);
        }


        /// <summary>
        /// Extension. Reads the Theme value from Properies.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static string Theme(this SessionFactoryHostContainer factory)
        {
            if (factory.TenantConfiguration == null)
                return string.Empty;
            if (string.IsNullOrEmpty(factory.TenantConfiguration.Properties["Theme"].ToString()))
                return string.Empty;

            return factory.TenantConfiguration.Properties["Theme"].ToString();
        }
    

        /// <summary>
        /// Converts an Collection to a generic List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection to convert.</param>
        /// <returns>Converted Collection to a List of T</returns>
        public static List<T> CollectionToList<T>(this System.Collections.ICollection collection)
        {
            var output = new List<T>(collection.Count);
            output.AddRange(collection.Cast<T>());

            return output;
        }


        /// <summary>
        /// Converts an array with a | as delimiter.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static List<string> CreateList(this string s)
        {
            var c = new char[] { '|' };
            return (s.Split(c)).CollectionToList<string>();
        }
    }
}
