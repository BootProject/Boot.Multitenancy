using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                    //Test connect to MySql.
                    var con = new MySqlConnection();
                    con.ConnectionString = "Server=localhost;Port=3306;Uid=boots;Pwd=boots;";
                    con.Open(); 
                    Console.ReadKey(true);

                    Console.WriteLine("Connected");

                    MySqlCommand cmd = new MySqlCommand("create database boots;", con);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Ready to drop database?");
                    Console.ReadKey(true);

                    cmd = new MySqlCommand("drop database boots;", con);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Dropped database boots.");
                    Console.ReadKey(true);
                    
              
                } catch(Exception ex) {

                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
    }
}
