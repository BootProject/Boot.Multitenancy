using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boot.Multitenancy.Extensions;

namespace Boot.Multitenancy.Filters
{
    /// <summary>
    /// 
    ///     DomainLevel attribute separates a class from each domain.
    ///     For e.g 
    ///         a domain A contains 
    ///             A.class, 
    ///             B.class. 
    ///         Domain B contains 
    ///             A.class, 
    ///             B.class but also 
    ///             C.class but domain A does'nt have C.class.
    ///             
    ///         How to separate this classes in code?
    ///         
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DomainLevel : Attribute
    {
        private List<string> Values { get; set; }
        
        public DomainLevel(string[] values)
        {
            Values = values.ToList();   
        }

        public bool PersistTable
        {
            get { return true; }
        }
    }
}
