using System;
using System.Collections.Generic;


namespace NHibernateProfiler.Common.Entity
{
    /// <summary>
    /// bstack @ 19/12/2009
    /// Prepared statement parameter
    /// </summary>
    public class PreparedStatementParameter
    {
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
    }
}
