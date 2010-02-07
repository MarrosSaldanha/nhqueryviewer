using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter.Parser
{
    /// <summary>
    /// bstack @ 17/01/2010
    /// Prepared statement parameter interface
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Must parse
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <returns></returns>
        bool MustParse(
            string[] sqlParts, 
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter>  parameters);

        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="sqlParts"></param>
        /// <returns></returns>
        List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> Parse(
            string[] sqlParts,
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> parameters);
    }
}
