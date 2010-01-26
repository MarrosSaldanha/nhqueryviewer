using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter.Parser
{
    /// <summary>
    /// bstack @ 17/01/2010
    /// Prepared statement parameter VALUE clause parser
    /// </summary>
    public class VALUEClause : 
        NHibernateProfiler.PreparedStatementParameter.Parser.Base,
        NHibernateProfiler.PreparedStatementParameter.Parser.IParser
    {
        /// <summary>
        /// Must parse
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <param name="parameters">Parameters, not used for this must parse test</param>
        /// <returns>True/False if it must be parsed or not</returns>
        public override bool MustParse(
            string[] sqlParts,
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> parameters)
        {
            if (sqlParts[2].Equals(" (")) { return true; }

            return false;
        }


        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>List of prepared statement parameters</returns>
        public override List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> Parse(
            string[] sqlParts,
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> parameters)
        {
            var _result = new List<NHibernateProfiler.Common.Entity.PreparedStatementParameter>();
            var _endBracketEncountered = false;
            var sqlPartsIndex = 2;

            while (!_endBracketEncountered)
            {
                sqlPartsIndex++;

                if (sqlParts[sqlPartsIndex] == ") VALUES (") { _endBracketEncountered = true; }
                else if (sqlParts[sqlPartsIndex] != ", ") { _result.Add(
                    new NHibernateProfiler.Common.Entity.PreparedStatementParameter()
                    { 
                        TableName = sqlParts[1], 
                        ColumnName = sqlParts[sqlPartsIndex] } ); }
            }

            return _result;
        }
    }
}