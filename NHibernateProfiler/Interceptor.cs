using NHibernate;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace NHibernateProfiler
{
	/// <summary>
	/// Interceptor, where intercepts occur throughout different parts of a typical nh session lifecycle
	/// NOTE: We currently only support one (1) session factory
	/// </summary>
	public class Interceptor : EmptyInterceptor
	{
		private NHibernate.AdoNet.Util.SqlStatementLogger c_sessionSqlStatementLogger;
		private NHibernate.Impl.SessionFactoryImpl c_currentSessionFactoryImpl;
		private Dictionary<Guid, NHibernateProfiler.Common.Entity.SessionDataComposite> c_sessionFactorySessions;


		/// <summary>
		/// Ctor
		/// </summary>
		public Interceptor()
		{
			this.c_sessionFactorySessions = new Dictionary<Guid, NHibernateProfiler.Common.Entity.SessionDataComposite>();
		}


		/// <summary>
		/// Set session
		/// </summary>
		/// <param name="session"></param>
		public override void SetSession(
			ISession session)
		{
			// Cache current sessions session factory impl
			this.c_currentSessionFactoryImpl = (NHibernate.Impl.SessionFactoryImpl)session.SessionFactory;

			var _sessionImpl = session.GetSessionImplementation();

			// Add sessionImpl if not already added
			if (!this.c_sessionFactorySessions.ContainsKey(_sessionImpl.SessionId))
			{
				var _sessionDataComposite = new NHibernateProfiler.Common.Entity.SessionDataComposite();
				_sessionDataComposite.Impl = _sessionImpl;
				_sessionDataComposite.Statistics = session.Statistics;

				this.c_sessionFactorySessions.Add(_sessionImpl.SessionId, _sessionDataComposite);
			}

			var _sessionImplFactorySettings = _sessionImpl.Factory.Settings;

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
			var _result = base.OnLoad(entity, id, state, propertyNames, types);

			this.UpdateSessionFactoryStatistics();

			return _result;
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
			base.OnDelete(entity, id, state, propertyNames, types);
			
			this.UpdateSessionFactoryStatistics();
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
			var _result = base.OnSave(entity, id, state, propertyNames, types);

			this.UpdateSessionFactoryStatistics();

			return _result;
		}


		/// <summary>
		/// Update session factory statistics
		/// </summary>
		private void UpdateSessionFactoryStatistics()
		{
			// Build session factory statistics
			var _sessionFactoryStatistics = NHibernateProfiler.SessionFactoryStatisticsBuilder.Build(
				this.c_currentSessionFactoryImpl, this.c_sessionFactorySessions);

			// Save session factory statistics
			NHibernateProfiler.Common.RepositoryFactory.Get().SaveSessionFactoryStatistics(_sessionFactoryStatistics);
		}
	}
}