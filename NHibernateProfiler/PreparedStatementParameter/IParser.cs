using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter
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
        bool MustParse(string[] sqlParts);

        /// <summary>
        /// Get parameters
        /// </summary>
        /// <param name="sqlParts"></param>
        /// <returns></returns>
        List<string> GetParameterNames(string[] sqlParts);
    }
}
