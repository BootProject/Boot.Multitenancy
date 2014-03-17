using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Boot.Multitenancy;
using Boot.Multitenancy.Factory;

using System.Data.SqlServerCe;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Boot.Web
{
    public static class BootConfig
    {
        public static void Init()
        {
            foreach (var tenant in Boot.Multitenancy.Host.ConfigCollection) 
            {
                var configuration = tenant.Value.Configuration;
                var con = new MySqlConnection("Server=127.0.0.1;Port=3306;Uid=boots;Pwd=boots;");
                con.Open();
                new MySqlCommand("CREATE DATABASE IF NOT EXISTS " + configuration.Key + ";", con).ExecuteNonQuery();
                con.Close();
            }
            Host.Init();
        }
    }
}