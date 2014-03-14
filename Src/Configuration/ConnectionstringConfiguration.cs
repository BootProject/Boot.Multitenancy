using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Configuration
{
    [Obsolete(message: "Not implemented, connectionstring is set in web.config.")]
    public static class ConnectionstringConfiguration
    {

        //SqlCe
        static readonly string SqlCeConnectionstring = "Data Source=|DataDirectory|{0}.sdf;Persist Security Info=False;"; //test only
        static readonly string MySqlConnectionstring = "Server=127.0.0.1;Port=3306;Database={0};Uid=boots;Pwd=boots;"; //test only

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
                    return string.Empty;
                case DbType.MySql5:
                    return string.Format(MySqlConnectionstring, key);
                default:
                    return string.Empty;
            }
        }
    }
}
