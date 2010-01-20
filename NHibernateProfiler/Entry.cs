using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.DynamicProxy;


namespace NHibernateProfiler
{
    public class Entry
    {
        /// <summary>
        /// Application connect
        /// </summary>
        public void ApplicationConnect()
        {
            List<Assembly> _callingApplicationAssemblies;
            Type _nhConfigurationType = null;

            // Get calling application assemblies
            _callingApplicationAssemblies = this.GetApplicationAssemblies(Assembly.GetCallingAssembly());

            // Get rid of unwanted assemblies to analyse
            // TODO: BS 05/12/2009

            // Get session factory object
            _callingApplicationAssemblies.ForEach(
                _callingApplicationAssembly => 
                    {
                        if(_callingApplicationAssembly.FullName.StartsWith("NHibernate,"))
                        {
                            var _types = _callingApplicationAssembly.GetTypes();
                            Array.ForEach(_types, type =>
                                {
                                    if (type.FullName.EndsWith(".Configuration"))
                                    {
                                        _nhConfigurationType = type;
                                        var x = _callingApplicationAssembly.GetType(_nhConfigurationType.FullName);
                                    }
                                }
                            );
                        
                        }
                    });

            ProxyGenerator _generator = new ProxyGenerator();

            //var _sessionFactoryInterceptor = new NHibernateProfiler.SessionFactoryInterceptor();
            //var proxy = _generator.CreateClassProxy(_nhConfigurationType, _sessionFactoryInterceptor);
            //_freezables.Add(proxy, freezableInterceptor);

        }


        /// <summary>
        /// Gets current application assemblies
        /// TODO: May eventually return a type with collection of assemblies + separate property for current calling assembly
        /// </summary>
        /// <param name="callingAssembly">The current calling assembly</param>
        /// <returns>List of current application assemblies</returns>
        private List<Assembly> GetApplicationAssemblies(Assembly callingAssembly)
        {
            List<Assembly> _result;

            // Instantiate assembly list adding calling assembly
            _result = new List<Assembly>() { callingAssembly };
            
            // Get names of all calling assembly referenced assemblies
            var _callingAssemblyReferencedAssemblyNames = callingAssembly.GetReferencedAssemblies();

            Array.ForEach(_callingAssemblyReferencedAssemblyNames,
                _callingAssemblyReferencedAssemblyName =>
                    _result.Add(Assembly.LoadWithPartialName(_callingAssemblyReferencedAssemblyName.FullName)));

            return _result;
        }
    }
}
