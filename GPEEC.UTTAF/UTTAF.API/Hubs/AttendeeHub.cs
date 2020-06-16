using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Hubs
{
	public class AttendeeHub : Hub
	{
		public AttendeeHub() { }

		[HubMethodName("Join")]
		public async Task JoinAtSessionAsync(AttendeeVO newAttendee) { }

		[HubMethodName("Leave")]
		public async Task LeaveAtSessionTaskAsync(AttendeeVO attendee) { }

		[HubMethodName("Attendees")]
		public async Task AttendeesInSessionTaskAsync(AttendeeVO session) { }
	}
}