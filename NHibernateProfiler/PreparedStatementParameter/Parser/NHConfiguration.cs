using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter.Parser
{
    /// <summary>
    /// bstack @ 26/01/2010
    /// NHibernate configuration object parser
    /// </summary>
    public class NHConfiguration : 
        NHibernateProfiler.PreparedStatementParameter.Parser.Base,
        NHibernateProfiler.PreparedStatementParameter.Parser.IParser
    {
        /// <summary>
        /// Must parse
        /// </summary>
        /// <param name="sqlParts">sqlParts, not used for this parse test</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>True/False if it must be parsed or not</returns>
        public override bool MustParse(
            string[] sqlParts,
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> parameters)
        {
            return parameters.Count > 0;
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
   
            // BS: TODO: Might need to set this via ctor for testing purposes
            var _configuration = NHibernateProfiler.Profiler.GetConfigurationObject;

            foreach (var parameter in parameters)
            {
                foreach (var _classMapping in _configuration.ClassMappings)
                {
                    if (_classMapping.Table.Name == parameter.TableName)
                    {
                        if (_classMapping.Key.ToString().Contains("(" + parameter.ColumnName + ")"))
                        {
                            parameter.Id = Guid.NewGuid();
                            parameter.EntityName = _classMapping.MappedClass.FullName;
                            parameter.EntityValue = "";
                            parameter.PropertyName = _classMapping.IdentifierProperty.Name;

                            _result.Add(parameter);
                        }
                        else
                        {
                            foreach (var _property in _classMapping.PropertyIterator)
                            {
                                if (_property.Value.ToString().Contains("(" + parameter.ColumnName + ")"))
                                {
                                    parameter.Id = Guid.NewGuid();
                                    //PreparedStatementId = preparedStatementId,
                                    parameter.EntityName = _classMapping.MappedClass.FullName;
                                    parameter.EntityValue = "";
									parameter.PropertyName = _property.Name;

                                    _result.Add(parameter);
                                }
                            }
                        }
                    }
                }
            }

            return _result;
        }
    }
}