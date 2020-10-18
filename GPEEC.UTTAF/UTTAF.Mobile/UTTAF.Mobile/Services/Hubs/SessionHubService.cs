using Microsoft.AspNetCore.SignalR.Client;

using System;

using UTTAF.Dependencies.Clients.Extensions;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Dependencies.Interfaces.RPC.Clients;

namespace UTTAF.Mobile.Services
{
	public class SessionHubService
	{
		private readonly HubConnection _connection;

		public SessionHubService(SessionConnection sessionConnection)
		{
			_connection = sessionConnection.Connection;
		}

		// On<T>

		public IDisposable DeletedSession(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.RemovedSessionAsync, action);
	}
}