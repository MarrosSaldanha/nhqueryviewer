using System;
using Xunit;
using System.Linq;


namespace NHibernateProfiler.UT
{
    /// <summary>
    /// Chain UT
    /// bstack @ 22/01/2010
    /// </summary>
    public class Chain
    {
        [Fact]
        public void Ctor_Valid()
        {
            var _chain = new NHibernateProfiler.PreparedStatementParameter.Chain();

            Assert.NotNull(_chain);
            Assert.NotNull(_chain.ParserCache);
            Assert.True(_chain.ParserCache.Count == 2);
            Assert.NotNull(_chain.ParserCache.FirstOrDefault(parser => parser.GetType() 
                == typeof(NHibernateProfiler.PreparedStatementParameter.VALUEClauseParser)));
            Assert.NotNull(_chain.ParserCache.FirstOrDefault(parser => parser.GetType()
                == typeof(NHibernateProfiler.PreparedStatementParameter.WHEREClauseParser)));
        }


        [Fact]
        public void ResolveParameters_Yes()
        {
            var _chain = new NHibernateProfiler.PreparedStatementParameter.Chain();
            

        }
    }
}
