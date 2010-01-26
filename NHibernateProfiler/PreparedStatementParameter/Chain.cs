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
        private readonly List<NHibernateProfiler.PreparedStatementParameter.Parser.IParser> c_parserCache;


        /// <summary>
        /// Parser cache, here for testing purposes
        /// </summary>
        public List<NHibernateProfiler.PreparedStatementParameter.Parser.IParser> ParserCache { get { return this.c_parserCache; } }

        
		/// <summary>
		/// Ctor, populates cache using reflection
		/// </summary>
		public Chain()
		{
			Type[] _parserConstructorTypes;
			object[] _parserConstructorArguments;
			Func<Type, bool> _candidateFilter;
			Action<Type> _repositoryPopulationAction;


			_parserConstructorTypes = new Type[] { };
			_parserConstructorArguments = new object[] { };

            this.c_parserCache = new List<NHibernateProfiler.PreparedStatementParameter.Parser.IParser>();

            // TODO: BS 25/01/2010 Can use Castle to register these interfaces (AlTypes....)
            // Get candidate filter, which will give all types that implement IParser interface
			_candidateFilter = candidateType => candidateType.GetInterfaces().FirstOrDefault(
				candidateTypeInterface =>
					candidateTypeInterface.FullName != null &&
                    candidateTypeInterface.FullName == "NHibernateProfiler.PreparedStatementParameter.Parser.IParser") != null;
			
            // Populate cache with all types that implement interface
			_repositoryPopulationAction =
				repositoryType =>
					this.c_parserCache.Add((NHibernateProfiler.PreparedStatementParameter.Parser.IParser) 
                    repositoryType.GetConstructor(_parserConstructorTypes).Invoke(_parserConstructorArguments));

			Assembly.GetExecutingAssembly().GetTypes().Where(_candidateFilter).MyForEach(_repositoryPopulationAction);

            this.c_parserCache.ForEach(parser =>
                {
                    if (parser.GetType().Name == "VALUEClause") { parser.Order = 1; }
                    if (parser.GetType().Name == "WHEREClause") { parser.Order = 2; }
                });
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
            int _startIndex = 1;

            foreach (var _parser in this.c_parserCache)
            {
                if (_parser.Order == _startIndex) 
                {
                    if(_parser.MustParse(subject, _parameters))
                    {
                       _parameters = _parser.Parse(subject, _parameters);
                    }
                };
            }

            return _parameters;
        }
	}
}