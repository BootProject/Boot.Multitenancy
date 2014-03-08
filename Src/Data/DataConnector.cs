using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Data
{
    public class DataConnector 
    {
        private DataConnector() { }

        public DataConnector(DbType dbtype)
        {
            switch (dbtype)
            {
                case DbType.SqlCe:
                    break;
                case DbType.SqlServer2008 :
                    break;
                case DbType.MySql5 :
                    break;
            }
        }
    }
}
