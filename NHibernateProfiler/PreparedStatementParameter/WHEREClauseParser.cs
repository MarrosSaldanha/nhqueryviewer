using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter
{
    /// <summary>
    /// bstack @ 17/01/2010
    /// Prepared statement parameter WHERE clause parser
    /// </summary>
    public class WHEREClauseParser : NHibernateProfiler.PreparedStatementParameter.IParser
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
        public List<NHibernateProfiler.Common.Entity.DatabaseInfo> GetParameterNames(string[] sqlParts)
        {
            var _result = new List<NHibernateProfiler.Common.Entity.DatabaseInfo>();

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

                    _result.Add(new NHibernateProfiler.Common.Entity.DatabaseInfo() { 
                        TableName =  _tableName, 
                        TableColumnName = _tableColumnName } );
                }
            }

            return _result;
        }


        private string GetTableName(string[] sqlParts, string alias)
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