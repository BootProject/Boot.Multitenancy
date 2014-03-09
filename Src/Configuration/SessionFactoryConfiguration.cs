using System;
using System.Collections.Generic;
using System.Configuration;


namespace Boot.Multitenancy.Configuration
{

    /// <summary>
    /// SessionFactoryConfiguration stores information about databases set in web.config.
    /// </summary>
    public class SessionFactoryConfiguration : ConfigurationSection
    {


        /// <summary>
        /// A list of databases, configurered in web.config.
        /// </summary>
        [ConfigurationProperty("databases", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(DatabaseCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public DatabaseCollection Databases
        {
            get { return (DatabaseCollection)base["databases"]; }
        }



        /// <summary>
        /// If to create databases by default.
        /// </summary>
        [ConfigurationProperty("persist")]
        public bool Persist
        {
            get { return (bool)this["persist"]; }
            set { this["persist"] = value; }
        }



        /// <summary>
        /// The namespace to look for IEntity.
        /// </summary>
        [ConfigurationProperty("namespace", IsRequired=true)]
        public string Namespace
        {
            get { return (string)this["namespace"]; }
            set { this["namespace"] = value; }
        }

    }
}
