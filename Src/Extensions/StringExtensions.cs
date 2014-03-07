using System;
using System.Web;

namespace Boot.Multitenancy.Extensions
{

    /// <summary>
    /// Contains various method extensions.
    /// </summary>
    public static class StringExtensions
    {


        /// <summary>
        /// Return a Key(domainname stripped to base domain, for e.g "domain.com" or "localhost" without port nmbr.)
        /// For e.g www.bootcms.com, bootcms.com
        /// </summary>
        /// <param name="s">string s to extract key from.</param>
        /// <returns>The domain name</returns>
        public static string Key(this string s)
        {
            return new Uri(HttpContext.Current.Request.Url.ToString()) //No need to throw exception here if not found.
                .GetComponents(
                UriComponents.AbsoluteUri &
                ~UriComponents.Port &
                ~UriComponents.Path &
                ~UriComponents.Scheme,
                UriFormat.UriEscaped)
                .GetBaseDomain();
        }



        /// <summary>
        /// Ensure that domain is in it's base form.
        /// </summary>
        /// <param name="s">The string to get base domain from.</param>
        /// <returns></returns>
        private static string GetBaseDomain(this string s)
        {
            return s.Replace("www", string.Empty);
        }
    }
}
