using Castle.Core.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;


namespace NHibernateProfiler
{
	/// <summary>
	/// bstack @ 17/03/2010
	/// NHibernate sql statement logger interceptor, intercepts via Castle DP generated proxy
	/// </summary>
	public class SqlStatementLoggerInterceptor : IInterceptor
	{
		/// <summary>
		/// Intercept
		/// </summary>
		/// <param name="invocation"></param>
		public void Intercept(
			IInvocation invocation)
		{
			var _sqlCommand = (System.Data.SqlClient.SqlCommand) invocation.Arguments[1];
			var _formatStyle = (NHibernate.AdoNet.Util.FormatStyle)invocation.Arguments[2]; 
			
			// Create prepared statement
			var _preparedStatement = new NHibernateProfiler.Common.Entity.PreparedStatement();
			_preparedStatement.Id = Guid.NewGuid();
			_preparedStatement.CreationTime = DateTime.Now;
			_preparedStatement.Sql = _sqlCommand.CommandText;
			_preparedStatement.Parameters = new List<NHibernateProfiler.Common.Entity.PreparedStatementParameter>();

			foreach (System.Data.SqlClient.SqlParameter _parameter in _sqlCommand.Parameters)
			{
				_preparedStatement.Parameters.Add(
					new NHibernateProfiler.Common.Entity.PreparedStatementParameter()
					{
						Id = Guid.NewGuid(),
						Name = _parameter.ToString(),
						Type = _parameter.SqlDbType.ToString(),
						Value = _parameter.Value.ToString()
					});
			}
			
			// Save prepared statement to db
			NHibernateProfiler.RepositoryFactory.Get().SavePreparedStatement(_preparedStatement);
			
			invocation.Proceed();
		}
	}
}
