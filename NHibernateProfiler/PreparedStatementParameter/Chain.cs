using NHibernateProfiler.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace NHibernateProfiler.PreparedStatementParameter
{
    /// <summary>
    /// bstack @ 20/01/2010
    /// Prepared statement parameter chain implementation
    /// </summary>
    public class Chain : NHibernateProfiler.PreparedStatementParameter.IChain
    {
		private readonly LinkedList<NHibernateProfiler.PreparedStatementParameter.Parser.IParser> c_parserCache;


        /// <summary>
        /// Parser cache, here for testing purposes
        /// </summary>
        public LinkedList<NHibernateProfiler.PreparedStatementParameter.Parser.IParser> ParserCache { get { return this.c_parserCache; } }



		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="parserCache">Parser cache</param>
		public Chain(
			LinkedList<NHibernateProfiler.PreparedStatementParameter.Parser.IParser> parserCache)
		{
			// Build linked list
			this.c_parserCache = parserCache;
		}


		/// <summary>
		/// Ctor
		/// </summary>
		public Chain()
		{
			// Build linked list
			this.c_parserCache = new LinkedList<NHibernateProfiler.PreparedStatementParameter.Parser.IParser>();
			this.c_parserCache.AddFirst(new NHibernateProfiler.PreparedStatementParameter.Parser.VALUEClause());
			this.c_parserCache.AddLast(new NHibernateProfiler.PreparedStatementParameter.Parser.WHEREClause());
			this.c_parserCache.AddLast(new NHibernateProfiler.PreparedStatementParameter.Parser.NHConfiguration());
		}


        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="sqlParts">sql parts</param>
        /// <returns>Parameters</returns>
        public List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> ResolveParameters(
            string[] subject)
        {
            var _parameters = new List<NHibernateProfiler.Common.Entity.PreparedStatementParameter>();
			var _currentNode = this.c_parserCache.First;

			while (_currentNode != null)
			{
				if (_currentNode.Value.MustParse(subject, _parameters))
				{
					_parameters = _currentNode.Value.Parse(subject, _parameters);
				}

				_currentNode = _currentNode.Next;
			}

            return _parameters;
        }
	}
}