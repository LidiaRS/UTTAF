using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace UTTAF.Dependencies.Extensions
{
	public static class HubConnectionBindExtensions
	{
		public static IDisposable BindOnInterface<T, TClient>(this HubConnection connection, Expression<Func<TClient, Func<T, Task>>> boundMethod, Action<T> handler)
			=> connection.On(GetMethodName(boundMethod), handler);

		public static IDisposable BindOnInterface<T1, T2, TClient>(this HubConnection connection, Expression<Func<TClient, Func<T1, T2, Task>>> boundMethod, Action<T1, T2> handler)
			=> connection.On(GetMethodName(boundMethod), handler);

		public static IDisposable BindOnInterface<T1, T2, T3, TClient>(this HubConnection connection, Expression<Func<TClient, Func<T1, T2, T3, Task>>> boundMethod, Action<T1, T2, T3> handler)
			=> connection.On(GetMethodName(boundMethod), handler);

		private static string GetMethodName<T>(Expression<T> boundMethod)
		{
			var unaryExpression = (UnaryExpression)boundMethod.Body;
			var methodCallExpression = (MethodCallExpression)unaryExpression.Operand;
			var methodInfoExpression = (ConstantExpression)methodCallExpression.Object;
			var methodInfo = (MethodInfo)methodInfoExpression.Value;
			return methodInfo.Name;
		}
	}
}