using NHibernate.AdoNet.Util;
using System;


namespace NHibernateProfiler.Common
{
	/// <summary>
	/// bstack @ 17/03/2010
	/// Proxy factory, generates proxies via Castle DP
	/// </summary>
	public class RepositoryFactory
	{
		private static NHibernateProfiler.Common.IRepository c_repository;


		/// <summary>
		/// Get
		/// </summary>
		/// <returns></returns>
		public static NHibernateProfiler.Common.IRepository Get()
		{
			// Instantiate if not already instantiated
			if (c_repository == null) 
			{
				c_repository = new NHibernateProfiler.Common.Repository(
					CreateSessionFactoryFromNonDefaultConfiguration());
			}

			return c_repository;
		}


		/// <summary>
		/// Create session factory from non-default configuration. This session factory is used by the profiler to log data
		/// </summary>
		/// <param name="strNonDefaultNHibernateConfigurationFilePath"></param>
		/// <returns></returns>
		private static NHibernate.ISessionFactory CreateSessionFactoryFromNonDefaultConfiguration()
		{
			// TODO: Move config to one place
			const string strNonDefaultNHibernateConfigurationFilePath = @"D:\Temp\hibernate.cfg.xml";

			return new NHibernate.Cfg.Configuration().Configure(
				strNonDefaultNHibernateConfigurationFilePath).BuildSessionFactory();
		}
	}
}
