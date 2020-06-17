using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.API.Business;
using UTTAF.Dependencies.Data.Clients.Interfaces;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Hubs
{
	public class SessionHub : Hub<ISessionClient>
	{
		private readonly ISessionBusiness _sessionBusiness;

		public SessionHub(ISessionBusiness sessionBusiness)
		{
			_sessionBusiness = sessionBusiness;
		}

		[HubMethodName("Create")]
		public async Task CreateSessionAsync(AuthSessionVO newSession)
		{
			if (await _sessionBusiness.ExistsBySessionReferenceTaskAsync(newSession.SessionReference))
			{
				await Clients.Caller.AlreadyExistsSessionAsync();
				return;
			}

			if (await _sessionBusiness.AddSessionTaskAsync(newSession) is AuthSessionVO addedSession)
			{
				await Clients.Caller.CreatedSessionAsync(addedSession);
				return;
			}

			await Clients.Caller.AlreadyExistsSessionAsync();
		}

		[HubMethodName("Start")]
		public async Task MarkSessionWithStartedAsync(AuthSessionVO session) { }

		[HubMethodName("Delete")]
		public async Task DeleteSessionAsync(AuthSessionVO session) { }
	}
}