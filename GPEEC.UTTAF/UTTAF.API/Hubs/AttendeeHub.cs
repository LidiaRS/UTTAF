using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Hubs
{
	public class AttendeeHub : Hub
	{
		public AttendeeHub()
		{
		}

		public async Task JoinAtSessionAsync(AttendeeVO newAttendee)
		{
		}

		public async Task LeaveAtSessionTaskAsync(AttendeeVO attendee)
		{
		}

		public async Task AttendeesInSessionTaskAsync(AttendeeVO session)
		{
		}
	}
}