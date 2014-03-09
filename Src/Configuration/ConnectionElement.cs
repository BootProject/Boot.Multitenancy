using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Configuration
{
    public class ConnectionElement
    {
        public string Name { get; set; }
        public string HostAddress { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Connectionstring { get; set; }
    }
}
