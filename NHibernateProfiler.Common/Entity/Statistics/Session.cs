using System;

namespace NHibernateProfiler.Common.Entity.Statistics
{
	/// <summary>
	/// bstack @ 19/12/2009
	/// Session statistics. This contains a subset of NHibernate.Engine.ISessionImplementor interface.
	/// </summary>
	public class Session
	{
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Active entity mode
		/// </summary>
		public string ActiveEntityMode { get; set; }
		
		/// <summary>
		/// Cache mode
		/// </summary>
		public string CacheMode { get; set; }

		/// <summary>
		/// Connection string
		/// </summary>
		public string ConnectionString { get; set; }

		/// <summary>
		/// Connection timeout
		/// </summary>
		public int ConnectionTimeout { get; set; }
		
		/// <summary>
		/// Connection state
		/// </summary>
		public string ConnectionState { get; set; }
		
		/// <summary>
		/// Flush mode
		/// </summary>
		public string FlushMode { get; set; }
		
		/// <summary>
		/// Is connected
		/// </summary>
		public bool IsConnected { get; set; }
		
		/// <summary>
		/// Is open
		/// </summary>
		public bool IsOpen { get; set; }

		/// <summary>
		/// Is closed
		/// </summary>
		public bool IsClosed { get; set; }

		/// <summary>
		/// Is event source
		/// </summary>
		public bool IsEventSource { get; set; }

		/// <summary>
		/// Collection count
		/// </summary>
		public int CollectionCount { get; set; }

		/// <summary>
		/// Entity count
		/// </summary>
		public int EntityCount { get; set; }

		/// <summary>
		/// Entity mode
		/// </summary>
		public string EntityMode { get; set; }

		/// <summary>
		/// Fetch profile
		/// </summary>
		public string FetchProfile { get; set; }

		/// <summary>
		/// Timestamp
		/// </summary>
		public long Timestamp { get; set; }

		/// <summary>
		/// Transaction in progress
		/// </summary>
		public bool TransactionInProgress { get; set; }
	}
}
