using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Interfaces.RPC.Clients
{
	public interface IAttendeeClient
	{
		Task JoinedAtSessionAsync(AttendeeVO attendee, SessionVO session, string message);

		Task NotJoinedAtSessionAsync(string message);

		Task NotExistsAttendeeWithThisNameAsync(string message);

		Task NotExistsAttendeeWithNameAsync(string message);

		Task ExitedAtSessionAsync(string message);
	}
}