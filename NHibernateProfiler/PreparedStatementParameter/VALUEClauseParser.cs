using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter
{
    /// <summary>
    /// bstack @ 17/01/2010
    /// Prepared statement parameter VALUE clause parser
    /// </summary>
    public class VALUEClauseParser : NHibernateProfiler.PreparedStatementParameter.IParser
    {
        /// <summary>
        /// Must parse
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <returns></returns>
        public bool MustParse(string[] sqlParts)
        {
            if (sqlParts[2].Equals(" (")) { return true; }

            return false;
        }


        /// <summary>
        /// Get parameters
        /// </summary>
        /// <param name="sqlParts"></param>
        /// <returns></returns>
        public List<string> GetParameterNames(string[] sqlParts)
        {
            var _result = new List<string>();
            var _endBracketEncountered = false;
            var sqlPartsIndex = 2;

            while (!_endBracketEncountered)
            {
                sqlPartsIndex++;

                if (sqlParts[sqlPartsIndex] == ") VALUES (") { _endBracketEncountered = true; }
                else if (sqlParts[sqlPartsIndex] != ", ") { _result.Add(sqlParts[sqlPartsIndex]); }
            }

            return _result;
        }
    }
}