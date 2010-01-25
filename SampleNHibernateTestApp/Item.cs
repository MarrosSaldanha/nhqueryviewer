using System;


namespace SampleNHibernateTestApp
{
    /// <summary>
    /// Item entity
    /// bstack @ 08/09/2009
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Customer Id
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FName { get; set; }

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