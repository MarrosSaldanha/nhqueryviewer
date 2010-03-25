using System;
using System.Collections.Generic;
using System.Linq;


namespace NHibernateProfiler
{
	/// <summary>
	/// bstack @ 19/03/2010
	/// Session factory statistics builder
	/// TODO: Need to rfactor this class - composed method pattern lacking
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

			// Get session factory stats from the database
			var _NHPSessionFactoryStatistics = NHibernateProfiler.Common.RepositoryFactory.Get()
				.GetSessionFactoryStatistics(sessionFactoryImpl.Uuid);

			// If session factory stats are null, instantiate
			if (_NHPSessionFactoryStatistics == null) _NHPSessionFactoryStatistics = 
				new NHibernateProfiler.Common.Entity.Statistics.SessionFactory();

			// Set session factory stats properties - these are set every time regardless of scenario
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
			
			// Spool through the profilers session factory sessions
			foreach (var _sessionId in sessionFactorySessions.Keys)
			{
				var _sessionImpl = sessionFactorySessions[_sessionId].Impl;
				var _actualSessionStatistics = sessionFactorySessions[_sessionId].Statistics;
				NHibernateProfiler.Common.Entity.Statistics.Session _currentSessionStatistics = null;

				// If sessions are not null, get current session statistics object out of session factory stats - it may or may not exist
				if (_NHPSessionFactoryStatistics.Sessions != null)
				{
					_currentSessionStatistics = _NHPSessionFactoryStatistics.Sessions.FirstOrDefault<NHibernateProfiler.Common.Entity.Statistics.Session>(
						session => session.Id == _sessionId);
				}

				var _doesSessionExistAlready = (_currentSessionStatistics != null); 

				// If session is closed, mark it as such and exit loop iteration
				if (_sessionImpl.IsClosed) 
				{
					_currentSessionStatistics.IsClosed = true;
					
					continue; 
				}

				// If the session does not exist, creat a new session
				if (!_doesSessionExistAlready)
				{
					_currentSessionStatistics = new NHibernateProfiler.Common.Entity.Statistics.Session();
				}

				// If we get to here, we set session properties regardless of scenario
				_currentSessionStatistics.ActiveEntityMode = _sessionImpl.EntityMode.ToString();
				_currentSessionStatistics.CacheMode = _sessionImpl.CacheMode.ToString();
				_currentSessionStatistics.CollectionCount = _actualSessionStatistics.CollectionCount;
				_currentSessionStatistics.ConnectionState = _sessionImpl.Connection.State.ToString();
				_currentSessionStatistics.ConnectionString = _sessionImpl.Connection.ConnectionString;
				_currentSessionStatistics.ConnectionTimeout = _sessionImpl.Connection.ConnectionTimeout;
				_currentSessionStatistics.EntityCount = _actualSessionStatistics.EntityCount;
				_currentSessionStatistics.EntityMode = _sessionImpl.EntityMode.ToString();
				_currentSessionStatistics.FetchProfile = _sessionImpl.FetchProfile;
				_currentSessionStatistics.FlushMode = _sessionImpl.FlushMode.ToString();
				_currentSessionStatistics.Id = _sessionImpl.SessionId;
				_currentSessionStatistics.IsClosed = _sessionImpl.IsClosed;
				_currentSessionStatistics.IsConnected = _sessionImpl.IsConnected;
				_currentSessionStatistics.IsEventSource = _sessionImpl.IsEventSource;
				_currentSessionStatistics.IsOpen = _sessionImpl.IsOpen;
				_currentSessionStatistics.Timestamp = _sessionImpl.Timestamp;
				_currentSessionStatistics.TransactionInProgress = _sessionImpl.TransactionInProgress;

				// Instantiate session factory stat session collection if null
				if (_NHPSessionFactoryStatistics.Sessions == null)
				{
					_NHPSessionFactoryStatistics.Sessions = new List<NHibernateProfiler.Common.Entity.Statistics.Session>();
				}

				// Add the session if not already added
				if (!_doesSessionExistAlready)
				{
					_NHPSessionFactoryStatistics.Sessions.Add(_currentSessionStatistics);
				}
			}

			return _NHPSessionFactoryStatistics;
		}
	}
}