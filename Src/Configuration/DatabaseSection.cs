using System;
using System.Collections.Generic;
using System.Configuration;
using Boot.Multitenancy.Extensions;

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




        /// <summary>
        /// Custom connectionstring.
        /// Use this to init your own connectionstring
        /// </summary>
        [ConfigurationProperty("connectionString", IsRequired = false)]
        public String Connectionstring
        {
            get { return (string)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }




        /// <summary>
        /// Domains, separated with |.
        /// Domains associated with this database.
        /// Note!! Is case sensitive.
        /// For eg. Boot.Multitenancy starts with Boot not boot.
        /// </summary>
        [ConfigurationProperty("domains", IsRequired = false)]
        public String Domains
        {
            get { return (string)this["domains"]; }
            set { this["domains"] = value; }
        }


        /// <summary>
        /// Helper. A list of domains.
        /// </summary>
        public List<string> DomainList
        {
            get { return Domains.CreateList(); }
        }
    }
}
