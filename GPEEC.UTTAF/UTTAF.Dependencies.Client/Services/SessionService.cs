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

		public async Task CreateSessionAsync(SessionVO newSession) =>
			await _connection.InvokeAsync(nameof(CreateSessionAsync), newSession);

		public async Task DeleteSessionAsync(SessionVO session) =>
			await _connection.InvokeAsync(nameof(DeleteSessionAsync), session);

		public async Task MarkSessionWithStartedAsync(SessionVO newSession) =>
			await _connection.InvokeAsync(nameof(MarkSessionWithStartedAsync), newSession);

		//On<T>

		public IDisposable CreatedSession(Action<SessionVO, string> action) =>
			_connection.BindOnInterface<SessionVO, string, ISessionClient>(x => x.CreatedSessionAsync, action);

		public IDisposable NotCreatedSession(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.NotCreatedSessionAsync, action);

		public IDisposable AlreadyExistsSession(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.AlreadyExistsSessionAsync, action);

		public IDisposable NotExistsThisSession(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.NotExistsThisSessionAsync, action);

		public IDisposable UpdatedSessionStatus(Action<SessionVO, string> action) =>
			_connection.BindOnInterface<SessionVO, string, ISessionClient>(x => x.UpdatedSessionStatusAsync, action);

		public IDisposable NotUpdatedSessionStatus(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.NotUpdatedSessionStatusAsync, action);

		public IDisposable RemovedSession(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.RemovedSessionAsync, action);

		public IDisposable NotRemovedSession(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.NotRemovedSessionAsync, action);
	}
}