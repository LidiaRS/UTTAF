using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Hubs
{
	public class AttendeeHub : Hub
	{
		public AttendeeHub() { }

		[HubMethodName("Join")]
		public async Task JoinAtSessionAsync(AttendeeModel newAttendee) { }

		[HubMethodName("Leave")]
		public async Task LeaveAtSessionTaskAsync(AttendeeModel attendee) { }

		[HubMethodName("Attendees")]
		public async Task AttendeesInSessionTaskAsync(AuthSessionModel session) { }
	}
}