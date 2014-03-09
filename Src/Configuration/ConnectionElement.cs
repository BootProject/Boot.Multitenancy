using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Boot.Multitenancy.Configuration
{
    /// <summary>
    /// 
    /// </summary>
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
    }
}
