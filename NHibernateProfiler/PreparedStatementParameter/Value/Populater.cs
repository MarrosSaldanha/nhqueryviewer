using System;
using System.Collections.Generic;


namespace NHibernateProfiler.PreparedStatementParameter.Value
{
    /// <summary>
    /// bstack @ 27/01/2010
    /// Prepared statement parameter value populater
    /// </summary>
    public class Populater : NHibernateProfiler.PreparedStatementParameter.Value.IPopulater
    {
        public void PopulateValues(
            List<NHibernateProfiler.Common.Entity.PreparedStatementParameter> preparedStatementParameters,
            object entityToGetValueFrom,
			Guid preparedStatementId)
        {
            preparedStatementParameters.ForEach(preparedStatementParameter =>
            {
				preparedStatementParameter.PreparedStatementId = preparedStatementId;

                Array.ForEach(entityToGetValueFrom.GetType().GetProperties(), property =>
                {
					if (property.Name == preparedStatementParameter.PropertyName)
					{
						preparedStatementParameter.EntityValue = property.GetValue(entityToGetValueFrom, null).ToString();
					}
					else
					{
						var _componentProperty =  property.GetValue(entityToGetValueFrom, null);

						if (_componentProperty != null)
						{
							Array.ForEach(_componentProperty.GetType().GetProperties(), innerComponentProperty =>
							{
								if (innerComponentProperty.Name == preparedStatementParameter.PropertyName)
								{
									preparedStatementParameter.EntityValue = innerComponentProperty.GetValue(_componentProperty, null).ToString();

								}
							});
						}
					}
                });
            });
        }
    }
}
