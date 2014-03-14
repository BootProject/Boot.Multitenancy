using System;
using Boot.Multitenancy.Intrastructure.Domain;


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
            Map(p => p.Html).CustomSqlType("text").Length(5000).Nullable();
            Map(p => p.Zone).CustomType<Int32>();
        }
    }

    internal static class SqlExt
    {
        public static string VarChar(int length)
        {
            return string.Format("AnsiString({0})", length);
        }
    }
}
