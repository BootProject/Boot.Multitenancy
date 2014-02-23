
using System;
using FluentNHibernate.Mapping;

namespace Boot.WebTest.Environment
{
    /// <summary>
    /// Test model for test MVC project.
    /// </summary>
    public class ModelTest
    {
        public virtual Int32 Id { get; set; }
        public virtual string Text { get; set; }
    }

    public class ModelTestMap : ClassMap<ModelTest>
    {
        public ModelTestMap()
        {
            Id(p => p.Id)
                .GeneratedBy
                  .Assigned()
                    .Not.Nullable();
            Map(p => p.Text);
        }
    }
}