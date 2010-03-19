using System;


namespace NHibernateProfiler.Common.Entity
{
	/// <summary>
	/// bstack @ 19/03/2010
	/// Session data composite
	/// </summary>
	public class SessionDataComposite
	{
		/// <summary>
		/// Impl
		/// </summary>
		public NHibernate.Engine.ISessionImplementor Impl { get; set; }
		
		/// <summary>
		/// Statistics
		/// </summary>
		public NHibernate.Stat.ISessionStatistics Statistics { get; set; }
	}
}
