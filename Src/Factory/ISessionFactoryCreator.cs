
using NHibernate;

namespace Boot.Multitenancy
{
    /// <summary>
    /// Interface ISessionFactoryCreator
    /// </summary>
    public interface ISessionFactoryCreator
    {
        //Nhibernate configuration(Fluent)
        ISessionFactory Create();
        //Current ITenant
        ISessionFactoryCreator Tenant { get; }
    }
}
