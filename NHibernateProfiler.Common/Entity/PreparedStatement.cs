using System;


namespace NHibernateProfiler.Common.Entity
{
    /// <summary>
    /// bstack @ 19/12/2009
    /// Prepared statement
    /// </summary>
    public class PreparedStatement
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Sql
        /// </summary>
        public string Sql { get; set; }
    }
}
