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
        /// Save prepared statement
        /// </summary>
        /// <param name="subject">Sql string</param>
        public void SavePreparedStatement(
            NHibernateProfiler.Common.Entity.PreparedStatement subject)
        {
            using (var _session = this.c_sessionFactory.OpenSession())
            {
                _session.Save(subject);
                _session.Flush();
            }
        }


        /// <summary>
        /// Get prepared statements
        /// </summary>
        /// <param name="subject">Sql string</param>
        public IList<NHibernateProfiler.Common.Entity.PreparedStatement> GetPreparedStatements()
        {
            using (ISession _session = this.c_sessionFactory.OpenSession())
            {
                return _session.CreateCriteria(typeof(NHibernateProfiler.Common.Entity.PreparedStatement))
                    .List<NHibernateProfiler.Common.Entity.PreparedStatement>();
            }
        }
    }
}