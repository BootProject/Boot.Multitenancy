using Boot.Multitenancy.Intrastructure.Domain;
using Boot.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boot.Web.ViewModels
{ 
    public class SetupViewModel
    {
        public List<Type> GetTypes{ get{ return new SetupModel().Types; } }
        public List<string> EntityNames { get; set; }
        public string SiteName { get; set; }
        public string FooterText { get; set; }
        public string Connectionstring { get; set; }
        public List<DbType> DbTypes { get; set; }
        public DbType SelectedType { get; set; }


        public SetupViewModel()
        {
            EntityNames = new List<string>();
            DbTypes = new List<DbType> { DbType.MySql5, DbType.SqlCe, DbType.SqlServer2008 };
        }
    }
}