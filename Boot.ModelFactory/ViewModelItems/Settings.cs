using System;
using Boot.Multitenancy.Intrastructure.Domain;

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
