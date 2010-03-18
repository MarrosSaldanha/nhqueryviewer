using System;
using System.Reflection;
using Castle.DynamicProxy;


namespace NHibernateProfiler.Proxy
{
	/// <summary>
	/// bstack @ 17/03/2010
	/// Proxy generation hook via Castle DP. 
	/// Used to decide whether to intercept methods or not
	/// </summary>
	public class GenerationHook: IProxyGenerationHook
	{
		/// <summary>
		/// Should intercept method
		/// </summary>
		/// <param name="type"></param>
		/// <param name="memberInfo"></param>
		/// <returns></returns>
		public bool ShouldInterceptMethod(
			Type type, 
			MethodInfo memberInfo) 
		{
			// We only want to intercept SQLStatementLogger method LogCommand which takes in 3 parameters
			return memberInfo.Name == "LogCommand" && memberInfo.GetParameters().Length == 3;
		}     
		

		/// <summary>
		/// Non-virtual member notification
		/// </summary>
		/// <param name="type"></param>
		/// <param name="memberInfo"></param>
		public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo) { }     
		

		/// <summary>
		/// Methods inspected
		/// </summary>
		public void MethodsInspected() { }
	}
}
