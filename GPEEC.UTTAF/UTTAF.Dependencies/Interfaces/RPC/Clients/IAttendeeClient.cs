using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Interfaces.RPC.Clients
{
	public interface IAttendeeClient
	{
		Task JoinedAtSessionAsync(AttendeeVO attendee, SessionVO session, string message);

		Task NotJoinedAtSessionAsync(string message);

		Task NotExistsAttendeeWithThisNameAsync(string message);

		Task ExitedAtSessionAsync(string message);

		Task SessionStartedAsync(SessionVO session, string message);
	}
}