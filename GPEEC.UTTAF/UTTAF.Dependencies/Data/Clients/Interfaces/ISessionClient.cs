using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Data.Clients.Interfaces
{
	public interface ISessionClient
	{
		Task CreatedSessionAsync(AuthSessionVO createdSession);

		Task NotCreatedSessionAsync();

		Task AlreadyExistsSessionAsync();
	}
}