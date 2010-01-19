using System;


namespace NHibernateProfiler.Common.Entity
{
    /// <summary>
    /// bstack @ 19/12/2009
    /// Prepared statement
    /// </summary>
    public class PreparedStatementParameter
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Prepared statement Id
        /// </summary>
        public Guid PreparedStatementId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
    }
}
