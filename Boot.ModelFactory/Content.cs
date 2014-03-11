using Boot.Multitenancy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.ModelFactory
{
    public class Content : IEntity
    {
        public virtual Int32 Id { get; set; }
        public virtual Int32 PageId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Html { get; set; }
        public virtual Region Zone { get; set; }
    }

    public class ContentMap : Entity<Content>
    {
        public ContentMap()
        {
            Id(x => x.Id)
               .Unique()
               .Column("Id")
               .Not.Nullable()
               .GeneratedBy.Identity()
               .CustomType<Int32>();
            Map(p => p.PageId);
            Map(p => p.Title);
            Map(p => p.Html);
            Map(p => p.Zone)
                .CustomType<Int32>();
        }
    }
}
