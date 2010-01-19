using System;
using NUnit.Framework;


namespace NHibernateProfiler.Common.UT
{
    /// <summary>
    /// bstack @ 17/01/2010
    /// Prepared statement parameter query parser UT
    /// </summary>
    [TestFixture]
    public class QueryParser
    {
        private NHibernateProfiler.Common.PreparedStatementParameter.WHEREClauseParser c_QueryParser;

        [SetUp]
        public void SetUp()
        {
            this.c_QueryParser = new NHibernateProfiler.Common.PreparedStatementParameter.WHEREClauseParser();
        }


        /// <summary>
        /// Get parameters
        /// </summary>
        /// <param name="sqlParts"></param>
        /// <returns></returns>
        [Test]
        public void GetParameterNames_NoParameters()
        {
            var _testQuery = "SELECT this_.Id as Id2_0_, this_.FirstName as FirstName2_0_, this_.LastName as LastName2_0_, this_.Age as Age2_0_ FROM BasicXMLMapping_Customer";


            //var _actualResult = this.c_QueryParser.GetParameterNames(_testQuery);

            //Assert.IsNotNull(_actualResult);
            //Assert.AreEqual(0, _actualResult.Count);
        }
    }
}
