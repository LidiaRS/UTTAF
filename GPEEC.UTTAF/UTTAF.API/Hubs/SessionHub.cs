using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Hubs
{
	public class SessionHub : Hub
	{
		public SessionHub() { }

		[HubMethodName("Create")]
		public async Task CreateSessionAsync(AuthSessionModel newSession) { }

		[HubMethodName("Start")]
		public async Task MarkSessionWithStartedAsync(AuthSessionModel session) { }

		[HubMethodName("Delete")]
		public async Task DeleteSessionAsync(AuthSessionModel session) { }
	}
}