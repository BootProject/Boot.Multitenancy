using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Boot.Multitenancy.Intrastructure
{
    /// <summary>
    /// Fluent dont understand Stringlength? Bug?, Anyway, this helper does the job!!
    /// </summary>
    public class StringColumnLengthConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(string)).Expect(x => x.Length == 0);
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Length(10000);
        }
    }
}
