using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;


namespace NHibernateProfiler
{
    /// <summary>
    /// Interceptor, where intercepts occur throughout different parts of a typical nh session lifecycle
    /// </summary>
    public class Interceptor : NHibernate.EmptyInterceptor
    {
        private readonly NHibernateProfiler.Common.IRepository c_repository;
        private readonly NHibernateProfiler.PreparedStatementParameter.IChain c_chain;

        private object c_entity;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository">Repository</param>
        /// <param name="chain">Chain of prepared statement parameter parsers</param>
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
            var temp = "";
            foreach(var part in sql.Parts)
            {
                temp += "\"" + part.ToString() + "\",";
            }

            
            var _sqlString =  base.OnPrepareStatement(sql);
            var _preparedStatementId = Guid.NewGuid();
            var _preparedStatements = this.GetParametersIfAnyExist(sql, _preparedStatementId);
            this.PopulateValuesForPreparedStatements(_preparedStatements);

            var _preparedStatement = new NHibernateProfiler.Common.Entity.PreparedStatement()
            {
                Id = _preparedStatementId,
                CreationTime = DateTime.Now,
                Sql = _sqlString.ToString(),
                Parameters = _preparedStatements
            };

            this.c_repository.SavePreparedStatement(_preparedStatement);

            return _sqlString;
        }


        public override bool OnSave(
            object entity, 
            object id, 
            object[] state, 
            string[] propertyNames, 
            NHibernate.Type.IType[] types)
        {
            this.c_entity = entity;

            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override void SetSession(NHibernate.ISession session)
        {
            base.SetSession(session);
        }


        public override bool OnLoad(
            object entity, 
            object id, 
            object[] state, 
            string[] propertyNames, 
            NHibernate.Type.IType[] types)
        {
            this.c_entity = entity;

            return base.OnLoad(entity, id, state, propertyNames, types);
        }

        public override void OnDelete(
            object entity, 
            object id, 
            object[] state, 
            string[] propertyNames, 
            NHibernate.Type.IType[] types)
        {
            this.c_entity = entity;

            base.OnDelete(entity, id, state, propertyNames, types);
        }


        /// <summary>
        /// Convert sql string to list
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>List of strings representing the prepared statement</returns>
        private string[] ConvertSqlPartsToStringArray(
            NHibernate.SqlCommand.SqlString subject)
        {
            var _sqlPartsAsStringArray = new string[subject.Parts.Count];
            var _i = 0;

            foreach (var _part in subject.Parts)
            {
                _sqlPartsAsStringArray[_i] = _part.ToString();

                _i++;
            }

            return _sqlPartsAsStringArray;
        }


        /// <summary>
        /// Get parameters if any exist
        /// </summary>
        /// <param name="sql"></param>
        private List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> GetParametersIfAnyExist(
            NHibernate.SqlCommand.SqlString sql,
            Guid preparedStatementId)
        {
            var _sqlPartsAsStringArray = this.ConvertSqlPartsToStringArray(sql);

            return this.c_chain.ResolveParameters(_sqlPartsAsStringArray);
        }


        private void PopulateValuesForPreparedStatements(
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> preparedStatements)
        {
            preparedStatements.ForEach(preparedStatement => {
                Array.ForEach(this.c_entity.GetType().GetProperties(), property => { 
                    if(property.Name == preparedStatement.PropertyName)
                    {
                        preparedStatement.EntityValue = property.GetValue(this.c_entity, null).ToString();
                    }
                });
            });
        }
    }
}