using System;
using System.Collections.Generic;
using System.Configuration;


namespace Boot.Multitenancy.Configuration
{
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
    }
}
