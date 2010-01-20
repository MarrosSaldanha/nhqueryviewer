using System;
using NHibernate.Cfg;


namespace NHibernateProfiler
{
    /// <summary>
    /// bstack @ 06/12/2009
    /// IProfiler interface, called by the application to be profiled
    /// </summary>
    public interface IProfiler
    {
        /// <summary>
        /// Initialise, passing back an NHibernate configuration object, that will be intercepted for
        /// statistics purposes
        /// </summary>
        /// <returns>NHibernate configuration object</returns>
        Configuration Initialise();
    }
}