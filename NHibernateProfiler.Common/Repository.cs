using NHibernate;
using System;
using System.Collections.Generic;


namespace NHibernateProfiler.Common
{
    /// <summary>
    /// bstack @ 19/12/2009
    /// Reads and writes serialised statistics object data to persistent storage
    /// </summary>
    public class Repository : NHibernateProfiler.Common.IRepository
    {
        private NHibernate.ISessionFactory c_sessionFactory;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="subject">File serialiser</param>
        public Repository(
            NHibernate.ISessionFactory subject)
        {
            this.c_sessionFactory = subject;
        }


		/// <summary>
		/// Save session factory statistics
		/// </summary>
		/// <param name="subject"></param>
		public void SaveSessionFactoryStatistics(
			NHibernateProfiler.Common.Entity.Statistics.SessionFactory subject)
		{
			using (var _session = this.c_sessionFactory.OpenSession())
			{
				using (var _transaction = _session.BeginTransaction())
				{
					_session.SaveOrUpdate(subject);
					_transaction.Commit();
				}
			}
		}


        /// <summary>
        /// Save prepared statement
        /// </summary>
        /// <param name="subject">Sql string</param>
        public void SavePreparedStatement(
            NHibernateProfiler.Common.Entity.PreparedStatement subject)
        {
            using (var _session = this.c_sessionFactory.OpenSession())
            {
				using (var _transaction = _session.BeginTransaction())
				{
					_session.Save(subject);
					_transaction.Commit();
				}
            }
        }
    }
}