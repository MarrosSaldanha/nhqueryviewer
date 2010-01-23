using System;
using System.Collections.Generic;
using System.Diagnostics;
using NHibernate.Cfg;


namespace NHibernateProfiler
{
    /// <summary>
    /// bstack @ 06/12/2009
    /// Profiler class, called by the application to be profiled.
    /// It sets up interceptor at configuration level and returns intercepted configuration object
    /// </summary>
    public class Profiler : NHibernateProfiler.IProfiler
    {
        private Configuration c_configuration;


        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns>NHibernate configuration object</returns>
        public static Configuration GetConfiguration()
        {
            return new Profiler().Initialise();
        }

        /// <summary>
        /// Initialise, passing back an NHibernate configuration object, that will be intercepted for
        /// statistics purposes
        /// </summary>
        /// <returns>NHibernate configuration object</returns>
        public Configuration Initialise()
        {
            if (this.c_configuration == null)
            {
                // TODO: BS 21/12/2009 Configure in castle...
                this.c_configuration = new Configuration().SetInterceptor(
                    new NHibernateProfiler.Interceptor(
                        new NHibernateProfiler.Common.Repository(
                            this.CreateSessionFactoryFromNonDefaultConfiguration(@"D:\Temp\hibernate.cfg.xml")),
                        new NHibernateProfiler.PreparedStatementParameter.Chain()));
            }

            this.StartProfilerApplicationProcess();

            return this.c_configuration;
        }


        /// <summary>
        /// Start profiler application process
        /// </summary>
        private void StartProfilerApplicationProcess()
        {
            //var _profilerApplicationProcess = new Process();

            //_profilerApplicationProcess.StartInfo.FileName = @"C:\Users\Billy\Documents\Visual Studio 2008\Projects\NHibernateProfiler\NHibernateProfiler.UI\bin\Debug\NHibernateProfiler.UI.exe";
            
            // TODO: BS We dont need any arguments yet
            //_profilerApplicationProcess.StartInfo.Arguments = "ProcessStart.cs";

            //_profilerApplicationProcess.Start();
        }


        /// <summary>
        /// Create session factory from non-default configuration. This session factory is used by the profiler to log data
        /// </summary>
        /// <param name="strNonDefaultNHibernateConfigurationFilePath"></param>
        /// <returns></returns>
        private NHibernate.ISessionFactory CreateSessionFactoryFromNonDefaultConfiguration(
            string strNonDefaultNHibernateConfigurationFilePath)
        {
            var _configuration = new NHibernate.Cfg.Configuration();

            _configuration.Configure(strNonDefaultNHibernateConfigurationFilePath);
            return _configuration.BuildSessionFactory();
        }
    }
}