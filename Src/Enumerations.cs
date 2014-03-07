using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Database type.
/// </summary>
public enum DbType
{
    SqlCe,
    SqlServer2008,
    MySql5
}

/// <summary>
/// Where to find configuration.
/// </summary>
public enum PersistFrom
{
    Config,
    Attribute,
    ByCode
}

/// <summary>
/// Use as regular key or domain management.
/// </summary>
public enum Usage
{
    Domain,
    Normal
}