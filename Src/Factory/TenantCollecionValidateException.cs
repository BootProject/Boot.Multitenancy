using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Factory
{
    public class TenantCollecionValidateException : Exception
    {
        public override string Message
        {
            get
            {
                return "This collection contain more or than one equal value. Solution! - Remove one of the values";
            }
        }
    }
}
