using System;
using System.Configuration;

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
        /// Creates a new DatabaseSection
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
        [ConfigurationProperty("name", IsRequired = true)]
        public String Name
        {
            get { return (String)this["name"]; }
            set { this["name"] = value; }
        }

        
        
        /// <summary>
        /// If to create this database
        /// </summary>
        [ConfigurationProperty("autoPersist", DefaultValue = false, IsRequired = true)]
        public Boolean AutoPersist
        {
            get { return (Boolean)this["autoPersist"]; }
            set { this["autoPersist"] = value; }
        }



        /// <summary>
        /// DbType
        /// </summary>
        [ConfigurationProperty("dbType", DefaultValue = DbType.SqlCe, IsRequired = true)]
        public DbType DbType
        {
            get { return (DbType)this["dbType"]; }
            set { this["dbType"] = value; }
        }


    }
}
