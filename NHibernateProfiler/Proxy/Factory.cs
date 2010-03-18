using Castle.DynamicProxy;
using NHibernate.AdoNet.Util;
using System;


namespace NHibernateProfiler.Proxy
{
	/// <summary>
	/// bstack @ 17/03/2010
	/// Proxy factory, generates proxies via Castle DP. 
	/// NOTE: Could make this class use generics, but not going to do so until required
	/// </summary>
	public class Factory
	{
		private static ProxyGenerator c_proxyGenerator;


		/// <summary>
		/// Get sql statement logger
		/// </summary>
		/// <returns></returns>
		public static SqlStatementLogger GetSqlStatementLogger()
		{
			// Instantiate if not already instantiated
			if (c_proxyGenerator == null) { c_proxyGenerator = new ProxyGenerator(); }

			// Create a proxy generation hook, enables us to filter the methods that are intercepted by the proxy
			var _proxyGenerationHook = new ProxyGenerationOptions(new NHibernateProfiler.Proxy.GenerationHook()); 
			
			return (SqlStatementLogger) c_proxyGenerator.CreateClassProxy(
				typeof(SqlStatementLogger),
				_proxyGenerationHook,
				new NHibernateProfiler.Proxy.SqlStatementLoggerInterceptor());
		}
	}
}
