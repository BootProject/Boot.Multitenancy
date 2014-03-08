
using NHibernate;

namespace Boot.Multitenancy
{
    /// <summary>
    /// Interface ISessionFactoryCreator
    /// </summary>
    public interface ISessionFactoryCreator
    {
        ISessionFactory Create();
    }
}
