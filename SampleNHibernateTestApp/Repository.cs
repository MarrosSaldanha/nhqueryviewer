using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate;


namespace SampleNHibernateTestApp
{
    /// <summary>
    /// Repository
    /// bstack @ 08/09/2009
    /// </summary>
    public class Repository
    {
        private readonly ISessionFactory c_sessionFactory;


        /// <summary>
        /// Ctor
        /// </summary>
        public Repository()
        {
            this.c_sessionFactory = (NHibernateProfiler.Profiler.GetConfiguration()).Configure().BuildSessionFactory();
        }


        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        public IList<SampleNHibernateTestApp.Customer> GetAll()
        {
            using (var _session = this.c_sessionFactory.OpenSession())
            {
                return _session.CreateCriteria(typeof(SampleNHibernateTestApp.Customer)).List<SampleNHibernateTestApp.Customer>();
            }
        }


        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        public SampleNHibernateTestApp.Customer GetById(
            Guid id)
        {
            using (var _session = this.c_sessionFactory.OpenSession())
            {
                return _session.CreateCriteria(typeof(SampleNHibernateTestApp.Customer))
               .Add(NHibernate.Criterion.Expression.Eq("Id", id))
               .UniqueResult<SampleNHibernateTestApp.Customer>();
            }
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        public IList<SampleNHibernateTestApp.Customer> GetByGreaterThanAge(
            int id)
        {
            using (var _session = this.c_sessionFactory.OpenSession())
            {
                return _session.CreateCriteria(typeof(SampleNHibernateTestApp.Customer))
               .Add(NHibernate.Criterion.Expression.Gt("Age", id))
               .List<SampleNHibernateTestApp.Customer>();
            }
        }


        /// <summary>
        /// Save customer
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public void SaveCustomer(
            SampleNHibernateTestApp.Customer subject)
        {
            using (var _session = this.c_sessionFactory.OpenSession())
            {
                _session.SaveOrUpdate(subject);
                _session.Flush();
            }
        }

    }
}
