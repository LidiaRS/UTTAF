using Microsoft.AspNetCore.SignalR.Client;

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UTTAF.Dependencies.Clients.Extensions
{
	public static class HubInvokeExtensions
	{
		public static async Task InvokeAsync<T>(this HubConnection connection, T arg, [CallerMemberName] string methodName = null)
		{
			await connection.InvokeAsync(methodName, arg);
		}
	}
}