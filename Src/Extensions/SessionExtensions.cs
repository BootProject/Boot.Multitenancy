using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NHibernate;
using NHibernate.Linq;

namespace Boot.Multitenancy.Extensions
{
    /// <summary>
    /// Contains helper for nHibernate ISessions
    /// </summary>
    public static partial class SessionExtensions
    {

        /// <summary>
        /// WorkWrapper. Extensions for ISession.
        /// </summary>
        /// <param name="session">The current Session</param>
        /// <param name="Work">Persist action work to function.</param>
        /// <returns></returns>
        private static ISession WorkWrapper(this ISession session, Action<ISession> Work)
        {
            var inLocalTransaction = true;

            if (!session.Transaction.IsActive)
                session.Transaction.Begin();
            else
                inLocalTransaction = false;

            try
            {
                Work(session);

                if (inLocalTransaction)
                    session.Transaction.Commit();
            }
            catch
            {
                if (inLocalTransaction)
                    session.Transaction.Rollback();
            }

            return session;
        }


        /// <summary>
        /// Save generic T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ISession Save<T>(this ISession session, T target) where T : class
        {
            return session.Save<T>(target, null);
        }


        /// <summary>
        /// Save generic T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="target"></param>
        /// <param name="saveCallback"></param>
        /// <returns></returns>
        public static ISession Save<T>(this ISession session, T target, Action<T> saveCallback) where T : class
        {
            return session.WorkWrapper(s =>
            {
                s.Save(target);

                if (saveCallback != null)
                    saveCallback(target);
            });
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ISession Update<T>(this ISession session, T target) where T : class
        {
            return session.Update<T>(target, null);
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="target"></param>
        /// <param name="updateCallback"></param>
        /// <returns></returns>
        public static ISession Update<T>(this ISession session, T target, Action<T> updateCallback) where T : class
        {
            return session.WorkWrapper(s =>
            {
                s.Update(target);

                if (updateCallback != null)
                    updateCallback(target);
            });
        }


        /// <summary>
        /// Delete T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ISession Delete<T>(this ISession session, T target) where T : class
        {
            return session.Delete<T>(target, null);
        }


        /// <summary>
        /// Delete T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="target"></param>
        /// <param name="deleteCallback"></param>
        /// <returns></returns>
        public static ISession Delete<T>(this ISession session, T target, Action<T> deleteCallback) where T : class
        {
            session.WorkWrapper(x =>
            {
                x.Delete(target);

                if (deleteCallback != null)
                    deleteCallback(target);
            });

            return session;
        }


        /// <summary>
        /// Delete T with expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ISession Delete<T>(this ISession session, Expression<Func<T, bool>> expression) where T : class
        {
            return session.Delete<T>(expression, null);
        }


        /// <summary>
        /// Delete T with expression and callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="expression"></param>
        /// <param name="deleteCallback"></param>
        /// <returns></returns>
        public static ISession Delete<T>(this ISession session, Expression<Func<T, bool>> expression, Action<T> deleteCallback) where T : class
        {
            session.WorkWrapper(x =>
            {
                var toDelete = session.Find<T>(expression);
                toDelete.ForEach(d =>
                {
                    x.Delete(d);

                    if (deleteCallback != null)
                        deleteCallback(d);
                });
            });

            return session;
        }


        /// <summary>
        /// List of generic T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static List<T> Find<T>(this ISession session, Expression<Func<T, bool>> expression) where T : class
        {
            var ret = new List<T>();
            var inLocalTransaction = true;

            if (!session.Transaction.IsActive)
                session.Transaction.Begin();
            else
                inLocalTransaction = false;

            try
            {
                ret = session.Query<T>().Where<T>(expression).ToList();

                if (inLocalTransaction)
                    session.Transaction.Commit();
            }
            catch
            {
                if (inLocalTransaction)
                    session.Transaction.Rollback();
            }

            return ret;
        }


        /// <summary>
        /// Gets all object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<T> All<T>(this ISession session) where T : class
        {
            var list = new List<T>();
            try
            {
                return session.Query<T>().ToList();
            }
            catch { return list; }


        }


        /// <summary>
        /// List of generic T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="expression"></param>
        /// <param name="resultListCallback"></param>
        /// <returns></returns>
        public static ISession Find<T>(this ISession session, Expression<Func<T, bool>> expression, Action<IList<T>> resultListCallback) where T : class
        {
            var ret = session.Find<T>(expression);
            if (resultListCallback != null)
                resultListCallback(ret);
            return session;
        }


        /// <summary>
        /// Paged list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalPages"></param>
        /// <returns></returns>
        public static IList<T> FindAll<T>(this ISession session, int pageNumber, int pageSize, out int totalPages) where T : class
        {
            totalPages = Convert.ToInt32(Math.Ceiling(session.QueryOver<T>().RowCount() / (double)pageSize));

            return session.CreateCriteria(typeof(T))
                .SetFirstResult((pageNumber - 1) * pageSize)
                .SetMaxResults(pageSize)
                .List<T>();
        }

       
        /// <summary>
        /// Perform a transaction
        /// </summary>
        /// <param name="session"></param>
        /// <param name="unitsOfWork"></param>
        /// <returns></returns>
        public static ISession WithTransaction(this ISession session, Action<ISession> unitsOfWork)
        {
            if (session.IsConnected == false)
            { //This should never happend. ??
               // Log.Debug("Lost session");
            }

            session.BeginTransaction();

            try
            {
                unitsOfWork(session);
                session.Transaction.Commit();

            }
            catch (Exception ex)
            {
                session.Transaction.Rollback();
            }
            return session;
        }
    }
}
