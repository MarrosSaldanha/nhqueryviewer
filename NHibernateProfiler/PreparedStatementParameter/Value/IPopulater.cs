using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter.Value
{
    /// <summary>
    /// bstack @ 27/01/2010
    /// Prepared statement parameter value populater interface
    /// </summary>
    public interface IPopulater
    {
        void PopulateValues(
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> preparedStatements,
            object entityToGetValueFrom,
			Guid preparedStatementId);
    }
}
