using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Hubs
{
	public class SessionHub : Hub
	{
		public SessionHub() { }

		[HubMethodName("Create")]
		public async Task CreateSessionAsync(AuthSessionVO newSession) { }

		[HubMethodName("Start")]
		public async Task MarkSessionWithStartedAsync(AuthSessionVO session) { }

		[HubMethodName("Delete")]
		public async Task DeleteSessionAsync(AuthSessionVO session) { }
	}
}