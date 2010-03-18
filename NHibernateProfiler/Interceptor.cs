using NHibernate;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace NHibernateProfiler
{
	/// <summary>
	/// Interceptor, where intercepts occur throughout different parts of a typical nh session lifecycle
	/// </summary>
	public class Interceptor : EmptyInterceptor
	{
		private NHibernate.AdoNet.Util.SqlStatementLogger c_sessionSqlStatementLogger;
		private static NHibernate.Impl.SessionFactoryImpl c_currentSessionFactoryImpl;


		/// <summary>
		/// Set session
		/// </summary>
		/// <param name="session"></param>
		public override void SetSession(
			ISession session)
		{
			// Cache current sessions session factory impl
			NHibernateProfiler.Interceptor.c_currentSessionFactoryImpl = 
				(NHibernate.Impl.SessionFactoryImpl)session.SessionFactory;
			
			var _sessionImplFactorySettings = session.GetSessionImplementation().Factory.Settings;

			// Get sql statement logger reference
			var _sqlStatementLoggerProperty = _sessionImplFactorySettings.GetType().GetProperty("SqlStatementLogger");

			// Set sql statement logger via reflection
			_sqlStatementLoggerProperty.SetValue(_sessionImplFactorySettings, NHibernateProfiler.Proxy.Factory.GetSqlStatementLogger(), null);

		}


		/// <summary>
		/// On load
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="id"></param>
		/// <param name="state"></param>
		/// <param name="propertyNames"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public override bool OnLoad(
			object entity, 
			object id, 
			object[] state, 
			string[] propertyNames, 
			NHibernate.Type.IType[] types)
		{
			this.UpdateSessionFactoryStatistics();

			return base.OnLoad(entity, id, state, propertyNames, types);
		}


		/// <summary>
		/// On delete
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="id"></param>
		/// <param name="state"></param>
		/// <param name="propertyNames"></param>
		/// <param name="types"></param>
		public override void OnDelete(
			object entity, 
			object id, 
			object[] state, 
			string[] propertyNames, 
			NHibernate.Type.IType[] types)
		{
			this.UpdateSessionFactoryStatistics();

			base.OnDelete(entity, id, state, propertyNames, types);
		} 


		/// <summary>
		/// On save
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="id"></param>
		/// <param name="state"></param>
		/// <param name="propertyNames"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public override bool OnSave(
			object entity, 
			object id, 
			object[] state, 
			string[] propertyNames, 
			NHibernate.Type.IType[] types)
		{
			this.UpdateSessionFactoryStatistics();

			return base.OnSave(entity, id, state, propertyNames, types);
		}


		/// <summary>
		/// Update session factory statistics
		/// </summary>
		private void UpdateSessionFactoryStatistics()
		{
			var _sessionFactoryStatistics = NHibernateProfiler.Interceptor.c_currentSessionFactoryImpl.Statistics;

			var _NHPSessionFactoryStatistics = new NHibernateProfiler.Common.Entity.Statistics.SessionFactory();
			_NHPSessionFactoryStatistics.UUID = NHibernateProfiler.Interceptor.c_currentSessionFactoryImpl.Uuid;
			_NHPSessionFactoryStatistics.CloseStatementCount = _sessionFactoryStatistics.CloseStatementCount;
			_NHPSessionFactoryStatistics.CollectionFetchCount = _sessionFactoryStatistics.CollectionFetchCount;
			_NHPSessionFactoryStatistics.CollectionLoadCount = _sessionFactoryStatistics.CollectionLoadCount;
			_NHPSessionFactoryStatistics.CollectionUpdateCount = _sessionFactoryStatistics.ConnectCount;
			_NHPSessionFactoryStatistics.ConnectCount = _sessionFactoryStatistics.CloseStatementCount;
			_NHPSessionFactoryStatistics.EntityDeleteCount = _sessionFactoryStatistics.EntityDeleteCount;
			_NHPSessionFactoryStatistics.EntityFetchCount = _sessionFactoryStatistics.EntityFetchCount;
			_NHPSessionFactoryStatistics.EntityInsertCount = _sessionFactoryStatistics.EntityInsertCount;
			_NHPSessionFactoryStatistics.EntityLoadCount = _sessionFactoryStatistics.EntityLoadCount;
			_NHPSessionFactoryStatistics.EntityUpdateCount = _sessionFactoryStatistics.EntityUpdateCount;
			_NHPSessionFactoryStatistics.FlushCount = _sessionFactoryStatistics.FlushCount;
			_NHPSessionFactoryStatistics.OptimisticFailureCount = _sessionFactoryStatistics.OptimisticFailureCount;
			_NHPSessionFactoryStatistics.PrepareStatementCount = _sessionFactoryStatistics.PrepareStatementCount;
			_NHPSessionFactoryStatistics.QueryExecutionCount = _sessionFactoryStatistics.QueryExecutionCount;
			_NHPSessionFactoryStatistics.QueryExecutionMaxTime = _sessionFactoryStatistics.QueryExecutionMaxTime;
			_NHPSessionFactoryStatistics.QueryExecutionMaxTimeQueryString = _sessionFactoryStatistics.QueryExecutionMaxTimeQueryString;
			_NHPSessionFactoryStatistics.SessionCloseCount = _sessionFactoryStatistics.SessionCloseCount;
			_NHPSessionFactoryStatistics.SessionOpenCount = _sessionFactoryStatistics.SessionOpenCount;
			_NHPSessionFactoryStatistics.StartTime = _sessionFactoryStatistics.StartTime;
			_NHPSessionFactoryStatistics.SuccessfulTransactionCount = _sessionFactoryStatistics.SuccessfulTransactionCount;
			_NHPSessionFactoryStatistics.TransactionCount = _sessionFactoryStatistics.TransactionCount;
			_NHPSessionFactoryStatistics.QueryExecutionCount = _sessionFactoryStatistics.QueryExecutionCount;
			
			// Save session factory statistics
			NHibernateProfiler.Common.RepositoryFactory.Get().SaveSessionFactoryStatistics(_NHPSessionFactoryStatistics);
		}
	}
}