using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Configuration
{
    /// <summary>
    /// Configuration element
    /// </summary>
    [Obsolete("Not implemented, and probably will not be. Leave out for now.")]
    public class ConnectionElement
    {

        /// <summary>
        /// The name or key to send to Boot.Multitenancy
        /// </summary>
        public string Name { get; set; }
        public string HostAddress { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Connectionstring { get; set; }
        public DbType DbType { get; set; }
        public string Theme { get; set; }
    }
}
