using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PersistAttribute : Attribute
    {
        private DbType DbType { get; set; }
        public Usage Usage { get; set; }
        private string Key { get; set; }
        private string DbName { get; set; }

        private PersistAttribute() { }

        public PersistAttribute(DbType type, Usage usage, string key)
        {

        }

        public PersistAttribute(DbType type, Usage usage, string key, string dbName)
        {
            new Registry().AutoList();
        }
    }
}
