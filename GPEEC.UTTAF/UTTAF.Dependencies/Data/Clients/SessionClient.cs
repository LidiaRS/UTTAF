using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Threading.Tasks;

using UTTAF.Dependencies.Data.Hubs.Interfaces;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Services.HubConnections;

namespace UTTAF.Dependencies.Data.Clients
{
	internal class SessionClient : ISessionHub
	{
		private readonly HubConnection _connection;

		public SessionClient(SessionConnection sessionConnection)
		{
			_connection = sessionConnection.Connection;
		}

		public Task CreateSessionAsync(SessionVO newSession)
		{
			throw new NotImplementedException();
		}

		public Task DeleteSessionAsync(SessionVO session)
		{
			throw new NotImplementedException();
		}

		public Task MarkSessionWithStartedAsync(SessionVO newSession)
		{
			throw new NotImplementedException();
		}
	}
}