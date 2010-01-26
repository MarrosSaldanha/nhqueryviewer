using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateProfiler.PreparedStatementParameter.Parser
{
    /// <summary>
    /// bstack @ 17/01/2010
    /// Prepared statement parameter abstract base parser
    /// </summary>
    public abstract class Base
    {
        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Must parse
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <returns></returns>
        public abstract bool MustParse(
            string[] sqlParts,
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> parameters);

        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="sqlParts"></param>
        /// <returns></returns>
        public abstract List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> Parse(
            string[] sqlParts,
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> parameters);
    }
}
