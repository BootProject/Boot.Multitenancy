using Boot.Multitenancy.Filters;
using Boot.Multitenancy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.ModelFactory
{
    /// <summary>
    /// Test to see how to separate tables from being generated in specific domains.
    /// </summary>
    //[DomainLevel(new string[]{"boot"})]
    public class DomainLevelClassTest //: IEntity
    {
        public virtual Int32 Id { get; set; }
        public virtual string Title { get; set; }
    }
    /*
    public class DomainLevelClassTestMap : Entity<DomainLevelClassTest>
    {
        public DomainLevelClassTestMap()
        {
            Id(x => x.Id)
               .Column("Id")
               .GeneratedBy.Assigned()
               .CustomType<Int32>();
            Map(p => p.Title);
        }
    }*/
}
