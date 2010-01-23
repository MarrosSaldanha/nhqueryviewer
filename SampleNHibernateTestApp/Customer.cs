using System;


namespace SampleNHibernateTestApp
{
    /// <summary>
    /// Customer entity
    /// bstack @ 08/09/2009
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; set; }
    }
}