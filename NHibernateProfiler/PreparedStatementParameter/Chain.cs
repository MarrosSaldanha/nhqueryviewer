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
        private readonly List<NHibernateProfiler.PreparedStatementParameter.IParser> c_parserCache;

        
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

            this.c_parserCache = new List<NHibernateProfiler.PreparedStatementParameter.IParser>();

            // Get candidate filter, which will give all types that implement IParser interface
			_candidateFilter = candidateType => candidateType.GetInterfaces().FirstOrDefault(
				candidateTypeInterface =>
					candidateTypeInterface.FullName != null &&
					candidateTypeInterface.FullName == "NHibernateProfiler.PreparedStatementParameter.IParser") != null;
			
            // Populate cache with all types that implement interface
			_repositoryPopulationAction =
				repositoryType =>
					this.c_parserCache.Add((NHibernateProfiler.PreparedStatementParameter.IParser) 
                    repositoryType.GetConstructor(_parserConstructorTypes).Invoke(_parserConstructorArguments));

			Assembly.GetExecutingAssembly().GetTypes().Where(_candidateFilter).MyForEach(_repositoryPopulationAction);
		}


        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="sqlParts">sql parts</param>
        /// <returns></returns>
        public List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> ResolveParameters(
            string[] subject)
        {
            var _parameterNames = new List<string>();

            foreach (var _parser in this.c_parserCache)
            {
                if (_parser.MustParse(subject)) { _parameterNames.AddRange(_parser.GetParameterNames(subject)); }
            }

            // BS 19/01/2010 Get parameter values for names (using reflection)
            return new List<NHibernateProfiler.Common.Entity.PreparedStatementParameter>();
        }
	}
}