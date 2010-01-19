using System;
using System.Collections.Generic;


namespace NHibernateProfiler.Common.Extension
{
	/// <summary>
	/// bstack @ 1/06/2010
	/// Collection foreach extension
	/// </summary>
	public static class Collection
	{
		/// <summary>
		/// For each extension
		/// </summary>
		/// <param name="self"></param>
		/// <param name="action"></param>
		public static void MyForEach<TSelf>(
			this IEnumerable<TSelf> self,
			Action<TSelf> action)
		{
			foreach (var _item in self) { action(_item); }
		}
	}
}