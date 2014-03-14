
namespace Boot.Multitenancy.Factory
{
    /// <summary>
    /// Interface for Tenants
    /// </summary>
    public interface ITenant
    {
        NHibernate.ISessionFactory CreateConfig();   
    }
}
