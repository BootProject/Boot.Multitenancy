using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Boot.Multitenancy;

namespace Boot.Web
{
    public static class BootConfig
    {
        public static void Init()
        {
            Host.Init();
        }
    }
}