
using NHibernate;

namespace Boot.Multitenancy
{
    /// <summary>
    /// Interface implemented for ISessionFactory
    /// </summary>
    public interface ISessionFactoryCreator
    {
        ISessionFactory Create();
    }
}
