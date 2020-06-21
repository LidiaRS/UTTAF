using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Data.Hubs.Interfaces
{
	public interface ISessionHub
	{
		Task CreateSessionAsync(SessionVO newSession);

		Task MarkSessionWithStartedAsync(SessionVO newSession);

		Task DeleteSessionAsync(SessionVO session);
	}
}