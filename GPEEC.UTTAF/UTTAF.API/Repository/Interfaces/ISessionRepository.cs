using System.Threading.Tasks;

using UTTAF.API.Models;

namespace UTTAF.API.Repository.Interfaces
{
	public interface ISessionRepository
	{
		Task<SessionModel> AddAsync(SessionModel session);

		Task<SessionModel> FindBySessionReferenceTaskAsync(string sessionReference);

		Task<SessionModel> ChangeStatusSessionTaskAsync(SessionModel currentSession, SessionModel newSession);

		Task RemoveTaskAsync(SessionModel session);
	}
}