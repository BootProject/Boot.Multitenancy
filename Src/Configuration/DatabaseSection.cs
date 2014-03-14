using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Creates a new DatabaseSection.
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
        /// Name of key(Database).
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public String Name
        {
            get { return (String)this["name"]; }
            set { this["name"] = value; }
        }


        /// <summary>
        /// If to create a database.
        /// </summary>
        [ConfigurationProperty("autoPersist", DefaultValue = false, IsRequired = true)]
        public Boolean AutoPersist
        {
            get { return (Boolean)this["autoPersist"]; }
            set { this["autoPersist"] = value; }
        }


        /// <summary>
        /// DbType, the type of database.
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
        [ConfigurationProperty("connectionstring", IsRequired = false)]
        public String Connectionstring
        {
            get { return (string)this["connectionstring"]; }
            set { this["connectionstring"] = value; }
        }


        /// <summary>
        /// Domains, separated with |.
        /// Domains associated with this database.
        /// Note!! Is case sensitive.
        /// For eg. Boot.Multitenancy starts with Boot not boot.
        /// </summary>
        [ConfigurationProperty("theme", IsRequired = false)]
        public String Theme
        {
            get { return (string)this["theme"]; }
            set { this["theme"] = value; }
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


        //Dictionary<string, object>
        [ConfigurationProperty("properties", IsRequired = false)]
        public String Properties
        {
            get { return (string)this["properties"]; }
            set { this["properties"] = value; }
        }


        /// <summary>
        /// Extension, a dictionary with properties.
        /// </summary>
        /// <returns>A dictionary with properties</returns>
        public Dictionary<string, object> PropertyList
        {
            get { return ParseProperties(); }
        }


        /// <summary>
        /// Extension, creates a dictionary from a string.
        /// </summary>
        /// <returns>A dictionary of properties</returns>
        private Dictionary<string, object> ParseProperties()
        {
            var delimiter = new char[] { '|' };
            var comma = new char[] { ',' };
            string[] props = Properties.Split(delimiter);

            var pair = new Dictionary<string, object>();

            foreach (var prop in props)
            {
                if (!string.IsNullOrEmpty(prop)) { 
                    var keyvaluePair = prop.Split(comma);
                    pair.Add(keyvaluePair[0], keyvaluePair[1]);
                }
            }

            return pair;
        }
    }
}
