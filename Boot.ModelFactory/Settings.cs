using Boot.Multitenancy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.ModelFactory
{
    public class Settings : IEntity
    {
        public virtual Int32 Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string FooterText { get; set; }
    }

    public class SettingsMap : Entity<Settings>
    {
        public SettingsMap()
        {
            Id(x => x.Id)
               .Unique()
               .Column("Id")
               .Not.Nullable()
               .GeneratedBy.Identity()
               .CustomType<Int32>();
            Map(p => p.Title);
            Map(p => p.FooterText);
        }
    }
}
