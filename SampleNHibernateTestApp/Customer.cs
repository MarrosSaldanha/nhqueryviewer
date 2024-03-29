﻿using System;
using System.Collections.Generic;


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
        public Guid TheId { get; set; }

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

        /// <summary>
        /// Creation attribute
        /// </summary>
        public SampleNHibernateTestApp.ModificationAttribute CreationAttribute { get; set; }


        /// <summary>
        /// Parameters
        /// </summary>
        public IList<SampleNHibernateTestApp.Item> Items { get; set; }
    }
}