using System;


namespace NHibernateProfiler.Common
{
    /// <summary>
    /// bstack @ 19/12/2009
    /// Repository interface
    /// </summary>
    public interface IRepository
    {
		/// <summary>
		/// Get session factory statistics
		/// </summary>
		/// <param name="UUID"></param>
		/// <returns></returns>
		NHibernateProfiler.Common.Entity.Statistics.SessionFactory GetSessionFactoryStatistics(
			string UUID);

		/// <summary>
		/// Save session factory statistics
		/// </summary>
		/// <param name="subject"></param>
		void SaveSessionFactoryStatistics(
			NHibernateProfiler.Common.Entity.Statistics.SessionFactory subject);


        /// <summary>
        /// Save prepared statement
        /// </summary>
        /// <param name="subject">Prepared statement</param>
        void SavePreparedStatement(
            NHibernateProfiler.Common.Entity.PreparedStatement subject);
    }
}
