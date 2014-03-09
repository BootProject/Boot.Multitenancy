﻿using Boot.Multitenancy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.ModelFactory
{
    public class Page : IEntity
    {
        public virtual Int32 Id { get; set; }
        public virtual Int32 ParentId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Url { get; set; }      //Used for redirect
        public virtual string MetaTitle { get; set; }
        public virtual string Controller { get; set; }
        public virtual string Action { get; set; }
        public virtual bool Active { get; set; }
    }

    public class PageMap : Entity<Page>
    {
        public PageMap()
        {
            Id(x => x.Id)
               .Column("Id")
               .GeneratedBy.Assigned()
               .CustomType<Int32>();
            Map(p => p.Title);
            Map(p => p.Url);
            Map(p => p.ParentId);
            Map(p => p.Active);
            Map(p => p.MetaTitle);
            Map(p => p.Controller);
            Map(p => p.Action);
        }
    }
}
