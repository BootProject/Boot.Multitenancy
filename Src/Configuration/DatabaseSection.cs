using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Configuration
{
    public class DatabaseSection : ConfigurationElement
    {
        public DatabaseSection() { }
        public DatabaseSection(String name, bool autoPersist, DbType dbtype)
        {
            this.Name = name;
            this.AutoPersist = autoPersist;
            this.DbType = dbtype;
        }

        [ConfigurationProperty("name")]
        public String Name
        {
            get { return (String)this["name"]; }
            set { this["name"] = value; }
        }
    
        [ConfigurationProperty("autoPersist", DefaultValue = false, IsRequired = false)]
        public Boolean AutoPersist
        {
            get { return (Boolean)this["autoPersist"]; }
            set { this["autoPersist"] = value; }
        }

        /// <summary>
        /// DbType
        /// </summary>
        [ConfigurationProperty("dbType", DefaultValue = DbType.SqlCe, IsRequired = false)]
        public DbType DbType
        {
            get { return (DbType)this["dbType"]; }
            set { this["dbType"] = value; }
        }
    }
}
