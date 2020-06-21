using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Threading.Tasks;

using UTTAF.Dependencies.Clients.Extensions;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Interfaces.Clients;
using UTTAF.Dependencies.Interfaces.Hubs;

namespace UTTAF.Dependencies.Clients.Services
{
	public class SessionService : ISessionHub
	{
		private readonly HubConnection _connection;

		public SessionService(SessionConnection sessionConnection)
		{
			_connection = sessionConnection.Connection;
		}

		//Invoke

		public async Task CreateSessionAsync(SessionVO newSession)
		{
			await _connection.InvokeAsync(nameof(CreateSessionAsync), newSession);
		}

		public async Task DeleteSessionAsync(SessionVO session)
		{
			await _connection.InvokeAsync(nameof(DeleteSessionAsync), session);
		}

		public async Task MarkSessionWithStartedAsync(SessionVO newSession)
		{
			await _connection.InvokeAsync(nameof(MarkSessionWithStartedAsync), newSession);
		}

		//On<T>

		public IDisposable CreatedSession(Action<SessionVO, string> action)
		{
			return _connection.BindOnInterface<SessionVO, string, ISessionClient>(x => x.CreatedSessionAsync, action);
		}

		public Task CreatedSessionAsync(SessionVO createdSession, string message)
		{
			throw new NotImplementedException();
		}

		public Task NotCreatedSessionAsync(string message)
		{
			throw new NotImplementedException();
		}

		public Task AlreadyExistsSessionAsync(string message)
		{
			throw new NotImplementedException();
		}

		public Task NotExistsThisSessionAsync(string message)
		{
			throw new NotImplementedException();
		}

		public Task UpdatedSessionStatusAsync(SessionVO updatedSession, string message)
		{
			throw new NotImplementedException();
		}

		public Task NotUpdatedSessionStatusAsync(string message)
		{
			throw new NotImplementedException();
		}

		public Task RemovedSessionAsync(string message)
		{
			throw new NotImplementedException();
		}

		public Task NotRemovedSessionAsync(string message)
		{
			throw new NotImplementedException();
		}
	}
}