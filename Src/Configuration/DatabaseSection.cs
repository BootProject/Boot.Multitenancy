using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Configuration
{

    /// <summary>
    /// DatabaseSection
    /// Configuration of a DatabaseElement
    /// </summary>
    public class DatabaseSection : ConfigurationElement
    {

        //Ctor
        public DatabaseSection() { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name, Key</param>
        /// <param name="autoPersist">If to create this object</param>
        /// <param name="dbtype">Database to use</param>
        public DatabaseSection(String name, bool autoPersist, DbType dbtype)
        {
            this.Name = name;
            this.AutoPersist = autoPersist;
            this.DbType = dbtype;
        }
        


        /// <summary>
        /// Name of key
        /// </summary>
        [ConfigurationProperty("name")]
        public String Name
        {
            get { return (String)this["name"]; }
            set { this["name"] = value; }
        }

        
        
        /// <summary>
        /// If to create this database
        /// </summary>
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
