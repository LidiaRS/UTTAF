using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Threading.Tasks;

using UTTAF.Dependencies.Clients.Extensions;
using UTTAF.Dependencies.Clients.Interfaces;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Interfaces.RPC.Clients;
using UTTAF.Dependencies.Interfaces.RPC.Hubs;

namespace UTTAF.Desktop.Services
{
	public class SessionService : ISessionHub, IConnectionManager
	{
		private readonly HubConnection _connection;

		public SessionService(SessionConnection sessionConnection)
		{
			_connection = sessionConnection.Connection;
		}

		public async Task ConnectAsync()
		{
			await _connection.StartAsync();
		}

		public async Task DesconeectAsync()
		{
			await _connection.StopAsync();
		}

		//Invoke

		public async Task CreateSessionAsync(SessionVO newSession) =>
			await _connection.InvokeAsync(newSession);

		public async Task MarkSessionWithStartedAsync(SessionVO newSessionStatus) =>
			await _connection.InvokeAsync(newSessionStatus);

		public async Task DeleteSessionAsync(string sessionReference) =>
			await _connection.InvokeAsync(sessionReference, nameof(DeleteSessionAsync));

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

		public IDisposable NotExistsAttendeeWithThisName(Action<string> action) =>
			_connection.BindOnInterface<string, IAttendeeClient>(x => x.NotExistsAttendeeWithThisNameAsync, action);
	}
}