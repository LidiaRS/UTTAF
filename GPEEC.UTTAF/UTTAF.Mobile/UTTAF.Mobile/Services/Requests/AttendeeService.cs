using Microsoft.AspNetCore.SignalR.Client;

using System.Threading.Tasks;

using UTTAF.Dependencies.Clients.Extensions;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Dependencies.Data.VOs;
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

		public async Task JoinAtSessionAsync(AttendeeVO newAttendee) => 
			await _connection.InvokeAsync(newAttendee);

		public async Task LeaveAtSessionAsync(AttendeeVO attendee) => 
			await _connection.InvokeAsync(attendee);
	}
}