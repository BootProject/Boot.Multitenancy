using System;
using System.Web;

using Boot.Multitenancy.Extensions;

namespace Boot.Multitenancy
{
    [Obsolete("Do not use, will be removed.")]
    public class SessionDomain
    {
        [Obsolete("Do not use, will be removed.")]
        public static string Domain { get; private set; }

        static SessionDomain()
        {
            Domain = string.Empty.Key();
        }
    }
}