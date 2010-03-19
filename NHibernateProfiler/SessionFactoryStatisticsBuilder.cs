using System;
using System.Collections.Generic;


namespace NHibernateProfiler
{
	/// <summary>
	/// bstack @ 19/03/2010
	/// Session factory statistics builder
	/// </summary>
	public static class SessionFactoryStatisticsBuilder
	{
		/// <summary>
		/// Build
		/// </summary>
		/// <param name="sessionFactoryImpl"></param>
		/// <param name="sessionFactorySessions"></param>
		/// <returns></returns>
		public static NHibernateProfiler.Common.Entity.Statistics.SessionFactory Build(
			NHibernate.Impl.SessionFactoryImpl sessionFactoryImpl,
			Dictionary<Guid, NHibernateProfiler.Common.Entity.SessionDataComposite> sessionFactorySessions)
		{
			var _sessionFactoryStatistics = sessionFactoryImpl.Statistics;

			var _NHPSessionFactoryStatistics = new NHibernateProfiler.Common.Entity.Statistics.SessionFactory();
			_NHPSessionFactoryStatistics.UUID = sessionFactoryImpl.Uuid;
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

			_NHPSessionFactoryStatistics.Sessions = new List<NHibernateProfiler.Common.Entity.Statistics.Session>();
			foreach (var _sessionId in sessionFactorySessions.Keys)
			{
				var _sessionImpl = sessionFactorySessions[_sessionId].Impl;
				var _actualSessionStatistics = sessionFactorySessions[_sessionId].Statistics;

				if (_sessionImpl.IsClosed) { continue; }

				var _sessionStatistics = new NHibernateProfiler.Common.Entity.Statistics.Session();
				_sessionStatistics.ActiveEntityMode = _sessionImpl.EntityMode.ToString();
				_sessionStatistics.CacheMode = _sessionImpl.CacheMode.ToString();
				_sessionStatistics.CollectionCount = _actualSessionStatistics.CollectionCount;
				_sessionStatistics.ConnectionState = _sessionImpl.Connection.State.ToString();
				_sessionStatistics.ConnectionString = _sessionImpl.Connection.ConnectionString;
				_sessionStatistics.ConnectionTimeout = _sessionImpl.Connection.ConnectionTimeout;
				_sessionStatistics.EntityCount = _actualSessionStatistics.EntityCount;
				_sessionStatistics.EntityMode = _sessionImpl.EntityMode.ToString();
				_sessionStatistics.FetchProfile = _sessionImpl.FetchProfile;
				_sessionStatistics.FlushMode = _sessionImpl.FlushMode.ToString();
				_sessionStatistics.Id = _sessionImpl.SessionId;
				_sessionStatistics.IsClosed = _sessionImpl.IsClosed;
				_sessionStatistics.IsConnected = _sessionImpl.IsConnected;
				_sessionStatistics.IsEventSource = _sessionImpl.IsEventSource;
				_sessionStatistics.IsOpen = _sessionImpl.IsOpen;
				_sessionStatistics.Timestamp = _sessionImpl.Timestamp;
				_sessionStatistics.TransactionInProgress = _sessionImpl.TransactionInProgress;

				_NHPSessionFactoryStatistics.Sessions.Add(_sessionStatistics);
			}

			return _NHPSessionFactoryStatistics;
		}
	}
}
