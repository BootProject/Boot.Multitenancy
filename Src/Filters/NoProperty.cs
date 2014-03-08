using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Filters
{
    /// <summary>
    /// Ignores Properties marked with NoProperty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NoProperty : Attribute  {  }

}
