using System;


namespace SampleNHibernateTestApp
{
	public class ModificationAttribute
	{
		/// <summary>
		/// By
		/// </summary>
        public string By { get; set; }

		/// <summary>
		/// Timestamp
		/// </summary>
        public DateTime Timestamp { get; set; }


		[Obsolete]
		protected ModificationAttribute()
		{
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="by"></param>
		/// <param name="timestamp"></param>
		public ModificationAttribute(
			string by,
			DateTime timestamp)
		{
			this.By = by;
			this.Timestamp = timestamp;
		}
	}
}

