using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlServerCe;
using System.Web.Http;
using System.Web.Http.Filters;
using MySql;
using MySql.Data.MySqlClient;
using log4net;
using System.Reflection;
using Host = Boot.Multitenancy.BootHost;

namespace Boot
{
    public static class BootHostWrapper
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Init()
        {
            (from configuration in Host.PreInit() select configuration)
                .ToList()
                    .ForEach(d => {
                        try{

                            //Slow slow slow... Use other database if you can.
                            if (d.DbType == DbType.SqlCe) { 
                                new SqlCeEngine(d.Connectionstring).CreateDatabase();
                            }

                            if (d.DbType == DbType.MySql5) {

                                var con = new MySqlConnection();
                                con.ConnectionString = "Server=localhost;Port=3306;Uid=boots;Pwd=boots;";
                                con.Open(); 

                                if (con.State == System.Data.ConnectionState.Open) {
                                    var c = new MySqlCommand("CREATE DATABASE IF NOT EXISTS " + d.Name + ";", con);
                                    c.ExecuteNonQuery();
                                }
                            }

                        } catch (Exception ex) { log.Debug(ex); }
                    });
            Host.Init();
        }
    }
}