using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Interfaces.RPC.Clients
{
	public interface ISessionClient
	{
		Task CreatedSessionAsync(SessionVO createdSession, string message);

		Task NotCreatedSessionAsync(string message);

		Task AlreadyExistsSessionAsync(string message);

		Task NotExistsThisSessionAsync(string message);

		Task UpdatedSessionStatusAsync(SessionVO updatedSession, string message);

		Task NotUpdatedSessionStatusAsync(string message);

		Task RemovedSessionAsync(string message);

		Task NotRemovedSessionAsync(string message);
	}
}