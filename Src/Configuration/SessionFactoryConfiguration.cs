using System;
using System.Collections.Generic;
using System.Configuration;


namespace Boot.Multitenancy.Configuration
{
    public class SessionFactoryConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("databases", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(DatabaseCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public DatabaseCollection Databases
        {
            get { return (DatabaseCollection)base["databases"]; }
        }
    }
}
