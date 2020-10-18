using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Threading.Tasks;

using UTTAF.Dependencies.Clients.Extensions;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Interfaces.RPC.Clients;
using UTTAF.Dependencies.Interfaces.RPC.Hubs;

namespace UTTAF.Mobile.Services
{
	public class AttendeeHubService : IAttendeeHub
	{
		private readonly HubConnection _connection;

		public AttendeeHubService(SessionConnection sessionConnection)
		{
			_connection = sessionConnection.Connection;
		}

		// Invokers

		public async Task JoinAtSessionAsync(AttendeeVO newAttendee) =>
			await _connection.InvokeAsync(newAttendee);

		public async Task LeaveAtSessionAsync(AttendeeVO attendee) =>
			await _connection.InvokeAsync(attendee);

		// On<T>

		public IDisposable JoinedAtSession(Action<AttendeeVO, SessionVO, string> action) =>
			_connection.BindOnInterface<AttendeeVO, SessionVO, string, IAttendeeClient>(x => x.JoinedAtSessionAsync, action);

		public IDisposable NotJoinedAtSession(Action<string> action) =>
			_connection.BindOnInterface<string, IAttendeeClient>(x => x.NotJoinedAtSessionAsync, action);

		public IDisposable NotExistsAttendeeWithThisName(Action<string> action) =>
			_connection.BindOnInterface<string, IAttendeeClient>(x => x.NotExistsAttendeeWithThisNameAsync, action);

		public IDisposable ExitedAtSession(Action<string> action) =>
			_connection.BindOnInterface<string, IAttendeeClient>(x => x.ExitedAtSessionAsync, action);

		public IDisposable NotExistsThisSession(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.NotExistsThisSessionAsync, action);

		public IDisposable UpdatedSessionStatus(Action<SessionVO, string> action) =>
			_connection.BindOnInterface<SessionVO, string, ISessionClient>(x => x.UpdatedSessionStatusAsync, action);

		public IDisposable NotUpdatedSessionStatus(Action<string> action) =>
			_connection.BindOnInterface<string, ISessionClient>(x => x.NotUpdatedSessionStatusAsync, action);
	}
}