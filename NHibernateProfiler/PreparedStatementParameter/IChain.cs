using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter
{
    /// <summary>
    /// bstack @ 20/01/2010
    /// Prepared statement parameter chain interface
    /// </summary>
    public interface IChain
    {
        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="sqlParts">Sql parts</param>
        /// <returns></returns>
        List<string> ResolveParameters(
            string[] subject);
    }
}
