using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Configuration
{
    /// <summary>
    /// DatabaseCollectionReader, reads configuration from web.config
    /// </summary>
    public class DatabaseCollectionReader
    {

        /// <summary>
        /// SessionFactoryConfiguration
        /// DatabaseElements and configuration.
        /// </summary>
        public static SessionFactoryConfiguration conf { get; set; }


        /// <summary>
        /// Value if to create databases.
        /// </summary>
        public static bool PersistDatabases { get; private set; }


        /// <summary>
        /// Runs configurationtests.
        /// </summary>
        static DatabaseCollectionReader()
        {
            if (ConfigurationSettings == null)
                throw new Exception("Configuration is null.");

            conf = ConfigurationSettings.GetSection("sessionFactoryConfiguration") as SessionFactoryConfiguration;

            if (conf == null)
                throw new NotImplementedException("SessionFactoryConfiguration is missing it's configuration.");

            PersistDatabases = conf.Persist;
        }


        /// <summary>
        /// Get current Configuration.
        /// </summary>
        private static System.Configuration.Configuration ConfigurationSettings
        {
            get
            {
                System.Configuration.Configuration configuration = null;
                if (System.Web.HttpContext.Current != null)
                    configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                else
                    configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                return configuration;
            }
        }
    }
}
