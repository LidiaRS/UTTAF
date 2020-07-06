using Microsoft.AspNetCore.SignalR.Client;

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UTTAF.Dependencies.Clients.Extensions
{
	public static class HubInvokeExtensions
	{
		public static async Task InvokeAsync(this HubConnection connection, [CallerMemberName] string methodName = null)
		{
			await connection.InvokeAsync(methodName);
		}

		public static async Task InvokeAsync(this HubConnection connection, object arg, [CallerMemberName] string methodName = null)
		{
			await connection.InvokeAsync(methodName, arg);
		}

		public static async Task InvokeAsync(this HubConnection connection, object arg1, object arg2, [CallerMemberName] string methodName = null)
		{
			await connection.InvokeAsync(methodName, arg1, arg2);
		}
	}
}