using FluentNHibernate.Mapping;

namespace Boot.Multitenancy.Intrastructure.Domain
{

    /// <summary>
    /// Entity
    /// </summary>
    /// <typeparam name="T">The type to map</typeparam>
    public abstract class Entity<T> : ClassMap<T> where T : class
    {
    }
}
