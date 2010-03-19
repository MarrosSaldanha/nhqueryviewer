using System;
using System.Collections.Generic;


namespace NHibernateProfiler.Common.Entity.Statistics
{
	/// <summary>
	/// bstack @ 19/12/2009
	/// Session factory statistics. This contains a subset of NHibernate.Stat.IStatistics interface.
	/// </summary>
	public class SessionFactory
	{
		/// <summary>
		/// UUID
		/// </summary>
		public string UUID { get; set; }

		/// <summary>
		/// The number of prepared statements that were released
		/// </summary>
		public long CloseStatementCount { get; set; }

		/// <summary>
		/// Global number of collections fetched
		/// </summary>
		public long CollectionFetchCount { get; set; }

		/// <summary>
		/// Global number of collections loaded
		/// </summary>
		public long CollectionLoadCount { get; set; }

		/// <summary>
		/// Global number of collections updated
		/// </summary>
		public long CollectionUpdateCount { get; set; }

		/// <summary>
		/// Get the global number of connections asked by the sessions (the actual number of connections used 
		/// may be much smaller depending whether you use a connection pool or not)
		/// </summary>
		public long ConnectCount { get; set; }

		/// <summary>
		/// Global number of entity deletes
		/// </summary>
		public long EntityDeleteCount { get; set; }

		/// <summary>
		/// Global number of entity fetchs
		/// </summary>
		public long EntityFetchCount { get; set; }
		
		/// <summary>
		/// Global number of entity inserts
		/// </summary>
		public long EntityInsertCount { get; set; }
		
		/// <summary>
		/// Global number of entity loads
		/// </summary>
		public long EntityLoadCount { get; set; }
		
		/// <summary>
		/// Global number of entity updates
		/// </summary>
		public long EntityUpdateCount { get; set; }
		
		/// <summary>
		/// Get the global number of flush executed by sessions (either implicit or explicit)
		/// </summary>
		public long FlushCount { get; set; }
		
		/// <summary>
		/// The number of StaleObjectStateExceptions that occurred
		/// </summary>
		public long OptimisticFailureCount { get; set; }
		
		/// <summary>
		/// The number of prepared statements that were acquired
		/// </summary>
		public long PrepareStatementCount { get; set; }
		
		/// <summary>
		/// Global number of executed queries
		/// </summary>
		public long QueryExecutionCount { get; set; }
		
		/// <summary>
		/// The System.TimeSpan of the slowest query.
		/// </summary>
		public TimeSpan QueryExecutionMaxTime { get; set; }
		
		/// <summary>
		/// The query string for the slowest query.
		/// </summary>
		public string QueryExecutionMaxTimeQueryString { get; set; }
		
		/// <summary>
		/// Global number of sessions closed
		/// </summary>
		public long SessionCloseCount { get; set; }
		
		/// <summary>
		/// Global number of sessions opened
		/// </summary>
		public long SessionOpenCount { get; set; }
		
		/// <summary>
		/// Start time
		/// </summary>
		public DateTime StartTime { get; set; }
		
		/// <summary>
		/// The number of transactions we know to have been successful
		/// </summary>
		public long SuccessfulTransactionCount { get; set; }
		
		/// <summary>
		/// The number of transactions we know to have completed
		/// </summary>
		public long TransactionCount { get; set; }

		/// <summary>
		/// Sessions
		/// </summary>
		public ICollection<NHibernateProfiler.Common.Entity.Statistics.Session> Sessions { get; set; }
	}
}
