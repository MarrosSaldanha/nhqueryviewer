using NHibernate;
using System;
using System.Reflection;


namespace NHibernateProfiler
{
	/// <summary>
	/// Interceptor, where intercepts occur throughout different parts of a typical nh session lifecycle
	/// </summary>
	public class Interceptor : EmptyInterceptor
	{
		private NHibernate.AdoNet.Util.SqlStatementLogger c_sessionSqlStatementLogger;


		public override void SetSession(ISession session)
		{
			var _sessionImplFactorySettings = session.GetSessionImplementation().Factory.Settings;

			// Get sql statement logger reference
			var _sqlStatementLoggerProperty = _sessionImplFactorySettings.GetType().GetProperty("SqlStatementLogger");

			// Set sql statement logger via reflection
			_sqlStatementLoggerProperty.SetValue(_sessionImplFactorySettings, NHibernateProfiler.Proxy.Factory.GetSqlStatementLogger(), null);

		}
	}
}