using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using NHibernate.Cfg;
using Castle.DynamicProxy;
using Castle.Core;


namespace NHibernateProfiler
{
    /// <summary>
    /// bstack @ 06/12/2009
    /// Profiler class, called by the application to be profiled.
    /// It sets up interceptor at configuration level and returns intercepted configuration object
    /// </summary>
    public class Profiler : NHibernateProfiler.IProfiler
    {
        private static Configuration c_configuration;


        internal static Configuration GetConfigurationObject { get { return c_configuration; } }


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
            if (c_configuration == null)
            {
                // TODO: BS 21/12/2009 Configure in castle...
                c_configuration = new Configuration().SetInterceptor(new NHibernateProfiler.Interceptor());
            }

            this.StartProfilerApplicationProcess();

            return c_configuration;
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
    }
}