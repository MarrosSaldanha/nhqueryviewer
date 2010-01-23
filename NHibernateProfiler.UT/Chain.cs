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
        public void ResolveParameters_Yes_For_Sql_Insert()
        {
            var _chain = new NHibernateProfiler.PreparedStatementParameter.Chain();
            var _insertSQLAsArray = NHibernateProfiler.UT.Factory.SqlString.ConvertSqlPartsToStringArray(
                NHibernateProfiler.UT.Factory.SqlString.Insert);

            var _resolvedParameterNames = _chain.ResolveParameters(_insertSQLAsArray);

            Assert.NotNull(_resolvedParameterNames);
            Assert.True(_resolvedParameterNames.Count == 4);
            Assert.True(_resolvedParameterNames.Contains("FirstName"));
            Assert.True(_resolvedParameterNames.Contains("LastName"));
            Assert.True(_resolvedParameterNames.Contains("Age"));
            Assert.True(_resolvedParameterNames.Contains("Id"));
        }


        [Fact]
        public void ResolveParameters_Yes_For_Sql_Select()
        {
            var _chain = new NHibernateProfiler.PreparedStatementParameter.Chain();
            var _selectSQLAsArray = NHibernateProfiler.UT.Factory.SqlString.ConvertSqlPartsToStringArray(
                NHibernateProfiler.UT.Factory.SqlString.Select);

            var _resolvedParameterNames = _chain.ResolveParameters(_selectSQLAsArray);

            Assert.NotNull(_resolvedParameterNames);
            Assert.True(_resolvedParameterNames.Count == 0);
        }


        [Fact(Skip = "Need to soft out the deletion of this. string")]
        public void ResolveParameters_Yes_For_Sql_Select_With_Where_Clause()
        {
            var _chain = new NHibernateProfiler.PreparedStatementParameter.Chain();
            var _selectSQLAsArray = NHibernateProfiler.UT.Factory.SqlString.ConvertSqlPartsToStringArray(
                NHibernateProfiler.UT.Factory.SqlString.SelectWithWhereClauseParameters);

            var _resolvedParameterNames = _chain.ResolveParameters(_selectSQLAsArray);

            Assert.NotNull(_resolvedParameterNames);
            Assert.True(_resolvedParameterNames.Count == 1);
            Assert.True(_resolvedParameterNames.Contains("Id"));
        }
    }
}
