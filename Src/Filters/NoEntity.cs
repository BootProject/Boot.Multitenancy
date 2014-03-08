using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Filters
{

    /// <summary>
    /// Tell the current Property to not be persisted to table.
    /// Ignores classes marked with NoEntity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NoEntity : Attribute   { }

}
