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
        /// <param name="subject">Sql string</param>
        void SavePreparedStatement(
            NHibernate.SqlCommand.SqlString subject);
    }
}
