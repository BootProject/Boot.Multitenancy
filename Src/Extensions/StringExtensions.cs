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
        /// Return a Key(domain name stripped to base domain, for e.g "domain.com" or "localhost" without port nmbr.)
        /// </summary>
        /// <param name="s"></param>
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
                .Replace("www.", string.Empty);
        }
    }
}
