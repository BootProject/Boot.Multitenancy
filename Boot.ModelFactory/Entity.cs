using Boot.Multitenancy.Infrastructure;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.ModelFactory
{
    public abstract class Entity<T> : ClassMap<T> where T : class
    {
    }
}
