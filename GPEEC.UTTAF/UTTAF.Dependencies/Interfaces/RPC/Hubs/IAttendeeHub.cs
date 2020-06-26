using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Interfaces.RPC.Hubs
{
	public interface IAttendeeHub
	{
		Task JoinAtSessionAsync(AttendeeVO newAttendee);

		Task LeaveAtSessionAsync(AttendeeVO attendee);
	}
}