using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boot.Multitenancy.Extensions;

namespace Boot.Multitenancy.Configuration
{
    
    /// <summary>
    /// 
    /// NOTE!!!! Not finished!!!! Only SqlCe is implemented. 
    /// 
    /// Stores information about known connectionstrings.
    /// Creates a connectionstring from a given name.
    /// </summary>
    public static class ConnectionstringConfiguration
    {

        //SqlCe
        static readonly string SqlCeConnectionstring = "Data Source=|DataDirectory|{0}.sdf;Persist Security Info=False;";
      


        /// <summary>
        /// Creates a connectionstring from DbType and key(the name of database to create).
        /// </summary>
        /// <param name="dbtype">The DbType</param>
        /// <param name="key">The key(name of database)</param>
        /// <returns>A connectionstring</returns>
        public static string CreateConnectionstring(DbType dbtype, string key)
        {
            if (Exist(key))
                return string.Empty;

            switch (dbtype)
            {
                case DbType.SqlCe:
                    return string.Format(SqlCeConnectionstring, key.Key());
                case DbType.SqlServer2008:
                    return string.Empty;
                case DbType.MySql5:
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }

        private static bool Exist(string path)
        {
            if (System.IO.File.Exists(path))
                return true;
            return false;
        }
    }
}
