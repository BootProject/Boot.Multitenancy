using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Configuration
{
    
    /// <summary>
    /// 
    /// NOTE!!!! Class not finished!!!!
    /// 
    /// Stores information about known connectionstrings.
    /// Creates a connectionstring from a given name.
    /// </summary>
    public static class ConnectionstringConfiguration
    {


        //SqlCe
        static readonly string SqlCeConnectionstring = "Data Source=|DataDirectory|{0}.sdf;Persist Security Info=False;";
        //SqlServer2008
        static readonly string SqlServer2008Connectionstring = "Data Source=|DataDirectory|{0}.sdf;Persist Security Info=False;";
        //MySql5
        static readonly string MySql5Connectionstring = "Data Source=|DataDirectory|{0}.sdf;Persist Security Info=False;";




        /// <summary>
        /// Creates a connectionstring from DbType and key(the name of database to create).
        /// </summary>
        /// <param name="dbtype">The DbType</param>
        /// <param name="key">The key(name of database)</param>
        /// <returns>A connectionstring</returns>
        public static string CreateConnectionstring(DbType dbtype, string key)
        {
            switch (dbtype)
            {
                case DbType.SqlCe:
                    return string.Format(SqlCeConnectionstring, key);
                case DbType.SqlServer2008:
                    return string.Format(SqlServer2008Connectionstring, key);
                case DbType.MySql5:
                    return string.Format(MySql5Connectionstring, key);
                default:
                    return string.Empty;
            }
        }
    }
}
