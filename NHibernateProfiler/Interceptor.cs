using System;
using System.Collections.Generic;


namespace NHibernateProfiler
{
    /// <summary>
    /// Interceptor, where intercepts occur throughout different parts of a typical nh session lifecycle
    /// </summary>
    public class Interceptor : NHibernate.EmptyInterceptor
    {
        private readonly NHibernateProfiler.Common.IRepository c_repository;
        private readonly NHibernateProfiler.PreparedStatementParameter.IChain c_chain;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="subject">Statistics writer</param>
        public Interceptor(
            NHibernateProfiler.Common.IRepository repository,
            NHibernateProfiler.PreparedStatementParameter.IChain chain)
        {
            this.c_repository = repository;
            this.c_chain = chain;
        }
 

        /// <summary>
        /// On prepare statement
        /// </summary>
        /// <param name="sql">Sql string</param>
        /// <returns></returns>
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(
            NHibernate.SqlCommand.SqlString sql)
        {
            var _sqlString =  base.OnPrepareStatement(sql);

            this.ParseString(sql);

            this.c_repository.SavePreparedStatement(_sqlString);

            return _sqlString;
        }

        public void ParseString(
            NHibernate.SqlCommand.SqlString sql)
        {
            var _sqlPartsAsStringArray = this.ConvertSqlStringToList(sql);

            var _resolvedParameterNames = this.c_chain.ResolveParameters(_sqlPartsAsStringArray.ToArray());
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            return base.OnSave(entity, id, state, propertyNames, types);
        }


        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            return base.OnLoad(entity, id, state, propertyNames, types);
        }


        /// <summary>
        /// Convert sql string to list
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>List of strings representing the prepared statement</returns>
        private List<string> ConvertSqlStringToList(
            NHibernate.SqlCommand.SqlString subject)
        {
            var _sqlPartsAsStringArray = new string[subject.Parts.Count];
            var _result = new List<string>();
            var _i = 0;

            foreach (var _part in subject.Parts)
            {
                _sqlPartsAsStringArray[_i] = _part.ToString();

                _i++;
            }

            return _result;
        }
    }
}