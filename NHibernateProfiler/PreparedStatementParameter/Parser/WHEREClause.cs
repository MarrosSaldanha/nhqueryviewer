using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter.Parser
{
    /// <summary>
    /// bstack @ 17/01/2010
    /// Prepared statement parameter WHERE clause parser
    /// </summary>
    public class WHEREClause :
        NHibernateProfiler.PreparedStatementParameter.Parser.Base,
        NHibernateProfiler.PreparedStatementParameter.Parser.IParser
    {
        /// <summary>
        /// Must parse
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <param name="parameters">Parameters, not used for this must parse test</param>
        /// <returns></returns>
        public override bool MustParse(
            string[] sqlParts,
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> parameters)
        {
			if (sqlParts == null) throw new ArgumentException();

            var _parametersExist = false;

            Array.ForEach(sqlParts, sqlPart => { if(sqlPart == "?") { _parametersExist = true; }});

            if (_parametersExist && !sqlParts[2].Equals(" (")) { return true; }

            return false;
        }


        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public override List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> Parse(
            string[] sqlParts,
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> parameters)
        {
            var _result = new List<NHibernateProfiler.Common.Entity.PreparedStatementParameter>();

            for (var _i = 0; _i < sqlParts.Length; _i++)
            {
                if(sqlParts[_i].ToString() == "?") 
                { 
                    // Get parameter string i.e. [table alias].[ColumnName]
                    var _parameterString = sqlParts[_i-2];

                    // Get separator character index
                    var _separatorCharacterIndex = _parameterString.LastIndexOf('.');

                    var _tableAlias = _parameterString.Substring(0, _separatorCharacterIndex);
                    var _tableColumnName = _parameterString.Substring(_separatorCharacterIndex + 1);

                    var _tableName = this.GetTableName(sqlParts, _tableAlias);

                    _result.Add(new NHibernateProfiler.Common.Entity.PreparedStatementParameter() {
                        TableName =  _tableName, 
                        ColumnName = _tableColumnName } );
                }
            }

            return _result;
        }


        /// <summary>
        /// Get table name
        /// </summary>
        /// <param name="sqlParts">sqlParts</param>
        /// <param name="alias">Alias</param>
        /// <returns></returns>
        private string GetTableName(
            string[] sqlParts, 
            string alias)
        {
            string _result = null;

            Array.ForEach(sqlParts, sqlPart =>
            {
                if (sqlPart.EndsWith(alias))
                {
                    _result = sqlPart.TrimEnd(alias.ToCharArray()).Trim();
                }
            });

            return _result;
        }
    }
}