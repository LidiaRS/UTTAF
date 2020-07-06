using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Threading.Tasks;

using UTTAF.Dependencies.Clients.Extensions;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Interfaces.RPC.Clients;
using UTTAF.Dependencies.Interfaces.RPC.Hubs;

namespace UTTAF.Mobile.Services.Requests
{
	public class AttendeeService : IAttendeeHub
	{
		private readonly HubConnection _connection;

		public AttendeeService(SessionConnection sessionConnection)
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
	}
}