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
		private NHibernate.ISession c_currentSessionFactoryStatisticsSession;


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
		/// <param name="UUID"></param>
		/// <returns>Session factory</returns>
		public NHibernateProfiler.Common.Entity.Statistics.SessionFactory GetSessionFactoryStatistics(
			string UUID)
		{
			this.c_currentSessionFactoryStatisticsSession = this.c_sessionFactory.OpenSession();
			
			return c_currentSessionFactoryStatisticsSession.CreateCriteria<NHibernateProfiler.Common.Entity.Statistics.SessionFactory>()
				.Add(NHibernate.Criterion.Expression.Eq("UUID", UUID))
				.UniqueResult<NHibernateProfiler.Common.Entity.Statistics.SessionFactory>();
		}


		/// <summary>
		/// Save session factory statistics
		/// </summary>
		/// <param name="subject"></param>
		public void SaveSessionFactoryStatistics(
			NHibernateProfiler.Common.Entity.Statistics.SessionFactory subject)
		{
			using (this.c_currentSessionFactoryStatisticsSession)
			{
				using (var _transaction = this.c_currentSessionFactoryStatisticsSession.BeginTransaction())
				{
					this.c_currentSessionFactoryStatisticsSession.SaveOrUpdate(subject);
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