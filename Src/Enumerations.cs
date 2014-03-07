using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum DbType
{
    SqlCe,
    SqlServer2008,
    MySql5
}


public enum PersistFrom
{
    Config,
    Attribute
}

public enum Usage
{
    Domain,
    Normal
}