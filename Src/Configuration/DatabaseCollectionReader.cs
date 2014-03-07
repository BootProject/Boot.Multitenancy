using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Boot.Multitenancy.Configuration
{

    /// <summary>
    /// DatabaseCollectionReader
    /// </summary>
    public class DatabaseCollectionReader
    {

        /// <summary>
        /// SessionFactoryConfiguration
        /// Holder of DatabaseElement
        /// </summary>
        public static SessionFactoryConfiguration conf { get; set; }



        /// <summary>
        /// Runs configuration.
        /// </summary>
        static DatabaseCollectionReader()
        {
            if (ConfigurationSettings == null)
                throw new Exception("configuration is null.");

            conf = ConfigurationSettings.GetSection("sessionFactoryConfiguration") as SessionFactoryConfiguration;

            if (conf == null)
               throw new Exception("SessionFactoryConfiguration is missing it's configuration.");
        }



        /// <summary>
        /// Get current Configuration.
        /// </summary>
        private static System.Configuration.Configuration ConfigurationSettings
        {
            get{
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
