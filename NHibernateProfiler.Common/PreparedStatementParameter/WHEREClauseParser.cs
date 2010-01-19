using System;
using System.Collections.Generic;


namespace NHibernateProfiler.Common.PreparedStatementParameter
{
    /// <summary>
    /// bstack @ 17/01/2010
    /// Prepared statement parameter WHERE clause parser
    /// </summary>
    public class WHEREClauseParser : NHibernateProfiler.Common.PreparedStatementParameter.IParser
    {
        /// <summary>
        /// Must parse
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <returns></returns>
        public bool MustParse(string[] sqlParts)
        {
            var _parametersExist = false;

            Array.ForEach(sqlParts, sqlPart => { if(sqlPart == "?") { _parametersExist = true; }});

            if (_parametersExist && !sqlParts[2].Equals(" (")) { return true; }

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

            for (var _i = 0; _i < sqlParts.Length; _i++)
            {
                if(sqlParts[_i].ToString() == "?") { _result.Add(sqlParts[_i-2]); }
            }

            return _result;
        }
    }
}