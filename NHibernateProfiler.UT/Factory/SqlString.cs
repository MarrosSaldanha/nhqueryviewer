using System;
using NHibernate;


namespace NHibernateProfiler.UT.Factory
{
    /// <summary>
    /// Sql string factory UT
    /// bstack @ 22/01/2010
    /// </summary>
    public static class SqlString
    {
        /// <summary>
        /// Insert sql string
        /// </summary>
        public static NHibernate.SqlCommand.SqlString Insert = new NHibernate.SqlCommand.SqlString(
            "INSERT INTO ",
            "BasicXMLMapping_Customer",
            " (",
            "FirstName",
            ", ",
            "LastName",
            ", ",
            "Age",
            ", ",
            "Id",
            ") VALUES (",
            "?",
            ", ",
            "?",
            ", ",
            "?",
            ", ",
            "?",
            ")");

        /// <summary>
        /// Select sql string
        /// </summary>
        public static NHibernate.SqlCommand.SqlString Select = new NHibernate.SqlCommand.SqlString(
            "SELECT ",
            "this_.Id as Id4_0_, this_.FirstName as FirstName4_0_, this_.LastName as LastName4_0_, this_.Age as Age4_0_",
            " FROM ",
            "BasicXMLMapping_Customer this_");


        /// <summary>
        /// Select with where clause parameters sql string
        /// </summary>
        public static NHibernate.SqlCommand.SqlString SelectWithWhereClauseParameters = new NHibernate.SqlCommand.SqlString(
            "SELECT ",
            "this_.Id as Id4_0_, this_.FirstName as FirstName4_0_, this_.LastName as LastName4_0_, this_.Age as Age4_0_",
            " FROM ",
            "BasicXMLMapping_Customer this_",
            " WHERE ",
            "this_.Id",
            " = ",
            "?");


        /// <summary>
        /// Convert sql string to list
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>List of strings representing the prepared statement</returns>
        public static string[] ConvertSqlPartsToStringArray(
            NHibernate.SqlCommand.SqlString subject)
        {
            var _sqlPartsAsStringArray = new string[subject.Parts.Count];
            var _i = 0;

            foreach (var _part in subject.Parts)
            {
                _sqlPartsAsStringArray[_i] = _part.ToString();

                _i++;
            }

            return _sqlPartsAsStringArray;
        }
    }
}
