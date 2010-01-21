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
        /// Save prepared statement
        /// </summary>
        /// <param name="subject">Prepared statement</param>
        void SavePreparedStatement(
            NHibernateProfiler.Common.Entity.PreparedStatement subject);
    }
}
