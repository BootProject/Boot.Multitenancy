using NHibernate;
using NHibernate.Context;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Boot.Multitenancy.Filter
{
    /// <summary>
    /// Opens and closes a SessionFactory.
    /// </summary>
    public class SessionFactoryAttribute : ActionFilterAttribute, IActionFilter
    {
        
        
        private ISessionFactory SessionFactory { get; set; }



        /// <summary>
        ///  Bind SessionFactory
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var session = SessionFactoryHostContainer.CurrentFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            session.BeginTransaction();
        }



        /// <summary>
        /// Close SessionFactory
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var session = SessionFactoryHostContainer.CurrentFactory.OpenSession();
            var transaction = session.Transaction;
            if (transaction != null && transaction.IsActive) {
                transaction.Commit();
            }
            session = CurrentSessionContext.Unbind(SessionFactory);
            session.Close();
        }
    }
}
