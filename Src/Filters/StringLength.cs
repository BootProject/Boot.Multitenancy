using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Filters
{
    /// <summary>
    /// Adds length value to string fields in database(TEXT)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StringLength : System.Attribute
    {
        public int Length = 0;
        public StringLength(int len)
        {
            Length = len;
        }
    }
}
